# MusicTree

## 役割
楽曲やフォルダ（BOX）の階層構造全体を管理し、選曲画面でのナビゲーション（フォーカスの移動、フォルダの開閉、難易度の切り替えなど）を制御するクラスです。

## 主要なフィールド
- `DifficultyLevel`: 現在選択されている難易度レベル。
- `Root`: 楽曲ツリーのルートノード.
- `FocusNode`: 現在選択されている（フォーカスがある）ノード。
- `FocusList`: 現在表示中の階層に含まれるノードのリスト。
- `OnFocusNodeChanged`: フォーカスが変わった際に発行されるイベント。

## メソッド一覧

### SearchAndAddToParentNode(Node parentNode, string folder, Action<string> onFileDetected = null)
指定されたフォルダを再帰的に検索し、楽曲ファイル（DTX等）やフォルダ構成をツリーに追加します。
- `set.def` ファイルがあれば、複数難易度をまとめた `SetNode` として処理します。
- `box.def` または `dtxfiles.` で始まるフォルダがあれば、`BoxNode` として処理します。
- 単体の楽曲ファイルは `MusicNode` として追加します。

### LoadMusicNode(WWW www, Node parentNode = null)
`WWW` オブジェクトから楽曲データを読み込み、ツリーに追加します。

### FocusOn(Node node)
指定したノードにフォーカスを合わせます。フォーカス変更に伴い、親階層のノードリストを `FocusList` に設定し、イベントを発行します。

### FocusNextNode() / FocusPreviousNode()
現在表示中のリスト内で、次または前のノードにフォーカスを移動させます。

### IncreaseDifficulty()
楽曲の難易度レベルを一段階上げます。選択中の楽曲にその難易度がない場合は、存在する難易度を探して適用します。

### GetClosestDifficultyLevel(SetNode setnode)
指定された `SetNode` 内で、現在の `DifficultyLevel` に最も近い、利用可能な難易度インデックスを返します。
