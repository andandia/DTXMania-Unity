# MainScript

## 役割
Unityのメインループ（Awake, Start, Update）を受け取り、プロジェクト全体のエントリーポイントとして機能するクラスです。各管理クラス（StageManager, SwitchManager, InputManager）の更新や、共通データの保持を行います。

## 主要なフィールド
- `Instance`: シングルトンインスタンス。
- `UIRoot`: UI要素を配置するルートとなるTransform。
- `DrumSound / WAVManager`: 音声再生管理コンポーネントへの参照。
- `MusicTree`: 楽曲の階層構造を管理するオブジェクト。
- `PlayingScore`: 現在選択中または演奏中の楽曲スコア。
- `CurrentGrade`: 現在の演奏成績。

## メソッド一覧

### Awake()
シングルトンインスタンスの設定を行い、自身がシーン遷移で破棄されないように設定します。

### Start()
音声管理コンポーネントの初期化を確認し、最初の画面である `LaunchStage` を開きます。

### Update()
`StageManager` と `SwitchManager` の更新処理を毎フレーム呼び出します。

### LateUpdate()
`InputManager` の更新処理を呼び出します（他クラスのUpdate処理後の入力を反映させるため）。
