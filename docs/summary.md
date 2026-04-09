# DTXMania-Unity プロジェクト概要サマリー

## プロジェクトについて
本プロジェクトは、ドラムシミュレーションゲーム「DTXMania」をUnity（2018.2.14f1）で再現したものです。オリジナルのDTXMania（C++版やDTXMania2）のリソースやフォーマットを継承しつつ、AndroidやPCなどのマルチプラットフォームでの動作を目指しています。

## 主要なモジュール構成

### 1. ゲームコア (Assets/Scripts)
- **MainScript.cs**: アプリケーションのエントリーポイント。主要なマネジャークラスの保持と更新を行います。
- **InputManager.cs**: キーボード、タッチ、MIDI入力の一元管理。
- **UserManager.cs / UserSettings.cs**: ユーザーごとのオートプレイ設定やハイスピード設定の管理。

### 2. 楽曲管理 (Assets/Scripts/Music)
- **MusicTree.cs**: 楽曲ファイルをスキャンし、フォルダ（BOX）や難易度（SetDef）を考慮した階層構造を構築します。
- **Node.cs / MusicNode.cs**: ツリーの各要素を表現し、楽曲メタデータやプレビュー情報を保持します。

### 3. UI・ステージ管理 (Assets/Scripts/UI)
- **StageManager.cs / SwitchManager.cs**: 画面（ステージ）の遷移と、遷移時のアニメーション（スイッチ）を制御します。
- **Activity.cs**: 階層構造を持つUI要素の基底クラス。
- **PlayingStage.cs**: 演奏ゲームのメインロジック。ヒット判定、スコア計算、ノーツ描画指示を行います。
- **SelectionStage.cs**: 楽曲選択画面のロジック。

### 4. オーディオ管理 (Assets/Scripts/Audio / WAVManager.cs / DrumSound.cs)
- **WAVManager.cs**: DTXファイルで指定された各チップのWAV音声を、IDに基づいて動的に読み込み・再生します。
- **DrumSound.cs**: 楽曲に依存しない、デフォルトのドラムパーツ音を管理します。
- **libbjxa.cs**: 特殊な音声フォーマットである `.xa` ファイルのデコーダー。

### 5. 入力デバイス (Assets/Scripts/MIDI)
- **UsbMidiDriver.cs**: AndroidのUSB MIDIホスト機能を利用して、外部電子ドラムなどのMIDI信号を受け取ります。

### 6. エディタ拡張 (Assets/Editor)
- **EditorTools.cs**: スプライトの一括設定やフォントアセットの生成など、開発を補助するツール群。

## 楽曲の読み込みと実行フロー
1. `LaunchStage` が起動し、設定されたフォルダから楽曲をスキャンして `MusicTree` を構築。
2. `TitleStage` 〜 `LoginStage` を経て `SelectionStage` で楽曲を選択。
3. `SongLoadStage` で、選択された曲の全WAVファイルを `WAVManager` のメモリ上にロード。
4. `PlayingStage` で演奏を開始。`ChipDrawingList` が時間経過に合わせてノーツの座標を計算し、`ChipSlot` が描画を担当。
5. 入力があった際、`PlayingStage` が `Grade` を更新し、`WAVManager` または `DrumSound` を通じて発音。
