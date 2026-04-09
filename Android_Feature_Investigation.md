# Android向け機能拡張の調査・実装方針レポート

本レポートは、Android端末（Android 14想定）において以下の2つの機能を実装するための調査結果と、具体的な実装方針をまとめたものです。

1. **パブリックフォルダからのDTXファイル・音源の自動読み込み**
2. **プレイ画面のレーンタッチによる演奏操作の実装**

---

## 1. パブリックフォルダからのDTX・音源ファイルの自動読み込み

### 現状の仕様と課題
現状のファイル読み込み処理は `Assets/Scripts/UI/Stage/LaunchStage.cs` にて実装されています。
- `GetExternalFilesDir()` メソッドで、`context.Call<AndroidJavaObject[]>("getExternalFilesDirs", null)` を用いてアプリ専用の外部領域（例: `Android/data/<package_name>/files/DTXFiles`）を取得して読み込み元として追加しています。
- Android 10以降（特にAndroid 14）の Scoped Storage の仕様により、アプリはデフォルトではユーザーが自由にアクセスできるパブリックフォルダ（`Downloads` や `Music` など）の全ファイルにアクセスすることができません。

### 実装方針
ストア配信を前提としないため、**「すべてのファイルへのアクセス権（MANAGE_EXTERNAL_STORAGE）」** をアプリに付与することで、ユーザーがPCから直接ファイルを配置しやすいパブリックフォルダから楽曲データを読み込むことが可能になります。

#### 具体的な改修ステップ

1. **AndroidManifest.xml の変更**
   Unityのプロジェクト設定でカスタム `AndroidManifest.xml` を出力し、以下のパーミッションを追加します。
   ```xml
   <uses-permission android:name="android.permission.READ_EXTERNAL_STORAGE" android:maxSdkVersion="32" />
   <uses-permission android:name="android.permission.MANAGE_EXTERNAL_STORAGE" />
   ```

2. **起動時の権限リクエストの実装**
   Android 11以降で `MANAGE_EXTERNAL_STORAGE` を使用するためには、ユーザーに設定画面を開かせて手動で許可を得る必要があります。
   `LaunchStage.cs` の初期化処理等の前に、以下のようなAndroid固有の権限チェック・リクエスト処理を追加します。
   ```csharp
   // 権限確認のイメージ
   if (!Environment.IsExternalStorageManager) {
       var intent = new AndroidJavaObject("android.content.Intent", "android.settings.MANAGE_APP_ALL_FILES_ACCESS_PERMISSION");
       // 設定画面を開く
   }
   ```

3. **読み込みパスの追加 (`LaunchStage.cs` の改修)**
   権限が許可されている前提で、パブリックフォルダのパス（例: `/storage/emulated/0/Download/DTXFiles`）をハードコード、もしくは環境変数から取得して `musicFolders` リストに追加します。
   ```csharp
   // 例: LaunchStage.cs の GetExternalFilesDir() またはそれに類する箇所
   #if !UNITY_EDITOR && UNITY_ANDROID
       // アプリ固有フォルダに加えて、パブリックディレクトリも追加
       var publicDownloads = Path.Combine("/storage/emulated/0/Download", "DTXFiles");
       if (Directory.Exists(publicDownloads)) {
           externDirs.Add(publicDownloads);
       }
   #endif
   ```
   これで、既存の `MusicTree.SearchAndAddToParentNode` が自動的に当該フォルダ内を再帰的に検索し、DTXファイルを追加してくれます。

---

## 2. 画面上のレーンタッチによる演奏入力の実装

### 現状の仕様と課題
- 入力処理は `InputManager.cs` で一元管理されており、キーボードやMIDI入力をポーリングして `DrumInputEvent` (叩かれたパッドの種類を示す `DrumInputType` を保持) をキューに追加しています。
- プレイ画面である `PlayingStage.cs` の `CheckInput()` メソッドでは、ノーツの判定時間枠内で `InputManager.Instance.GetDrumInput(DrumInputType)` を呼び出し、一致する入力があればノーツを処理（HITやOK等の判定）する仕組みです。
- 現状は画面タッチに関する入力処理がありません。

### 実装方針
既存の判定ロジック（`PlayingStage` 内の処理）には**一切手を加えず**、UIの各レーン部分へのタッチを検知し、それを `DrumInputEvent` として `InputManager` に流し込む（インジェクトする）アプローチが最も安全かつ確実です。

#### 具体的な改修ステップ

1. **タッチ入力用のコライダー / Raycast Target の配置**
   プレイ画面における各レーン（Snare, Tom, Cymbal 等）のUIコンポーネント、もしくはそれらを覆う透明なUI要素に対して、タッチイベントを受け取れるようにします（例：`Graphic Raycaster` や `BoxCollider2D` を配置）。
   既存のUI階層（`CenterPanel/ChipsPanel` 等）の背面に、レーン分割された透明なボタン（EventTrigger）を敷き詰める手法が簡単です。

2. **タッチ検知とイベントの注入**
   各レーンがタッチされた際（`PointerDown` 等のイベント発火時、もしくは `Update` 内での `Input.GetTouch` / `Input.touches` の処理時）に、該当するレーンの `DrumInputType` を特定します。
   特定した `DrumInputType` を、すでに実装されている `InputManager.Instance.EnqueueDrumInputEvent()` を利用して流し込みます。

   **実装イメージ:**
   ```csharp
   // タッチ入力監視用の新しいスクリプト (例: TouchInputHandler.cs)
   void Update() {
       foreach (Touch touch in Input.touches) {
           if (touch.phase == TouchPhase.Began) {
               DrumInputType inputType = DetermineLaneFromTouchPosition(touch.position);
               if (inputType != DrumInputType.Unknown) {
                   InputManager.Instance.EnqueueDrumInputEvent(
                       InputManager.DrumInputEvent.New(0, 0, inputType)
                   );
               }
           }
       }
   }
   ```
   ※`DetermineLaneFromTouchPosition` の部分は、画面のX座標に基づいてどのレーン（例：左端なら HiHat、中央なら Snare 等）かを判定するロジックを組み込みます。ユーザー設定に応じたレーン配置 (`DrumChipPropertyManager`) を考慮するとより完璧です。

3. **マルチタッチの対応**
   Android端末で複数レーンを同時にタッチ（同時押し）するケースがあるため、必ず `Input.touches` をループで回し、すべてのタッチの `Began` フェーズを処理してそれぞれキューに入れる必要があります。上記イメージの実装であればマルチタッチにも自然に対応できます。

---

### まとめ

1. **ファイル自動読み込み:**
   Androidのフルストレージアクセス権限 (`MANAGE_EXTERNAL_STORAGE`) を取得したうえで、`LaunchStage.cs` 内の検索先パスに `/storage/emulated/0/Download/DTXFiles` などのパブリックなパスを直接追加するだけで実現可能です。
2. **タッチ入力:**
   既存の複雑な判定ロジックには触れず、画面タッチの座標からレーンを割り出し、`InputManager.Instance.EnqueueDrumInputEvent()` を用いてキー・MIDI入力と同様のイベントキューに流し込む（アダプターパターン）実装により、安全に組み込むことができます。
