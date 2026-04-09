# Node

## 役割
楽曲ツリーを構成する要素の基底クラスです。楽曲（`MusicNode`）やフォルダ（`BoxNode`）に共通する属性（タイトル、親・子ノード、難易度情報、プレビュー画像・音声パスなど）を保持します。

## 主要なフィールド
- `Parent`: 親ノードへの参照。
- `ChildNodeList`: 子ノードのリスト。
- `Difficulty`: 難易度レベルごとのラベルと数値の配列。
- `Title / SubTitle`: 表示名とサブタイトル。
- `PreviewImagePath / PreviewAudioPath`: プレビュー用の画像および音声ファイルのパス。
- `PreviewSprite`: 読み込まれたプレビュー画像の `Sprite`。

## メソッド一覧

### Node() (コンストラクタ)
難易度情報の配列を初期化します。

### PlayPreviewAudio() / StopPreviewAudio()
プレビュー音声の再生・停止を行います（現在はパスの有無を確認するのみ）。

### PreNode / NextNode (プロパティ)
同じ階層における一つ前、または一つ後のノードを返します。リストの末尾と先頭はループします。

### OnLoadPreviewImage(WWW www)
`WWW` オブジェクトから読み込まれたテクスチャを受け取り、`PreviewSprite` を作成します。
