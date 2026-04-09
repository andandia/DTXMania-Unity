# BoxNode

## 役割
楽曲をまとめるフォルダ（BOX）を表すノードクラスです。`box.def` ファイルの設定を読み込み、選曲画面でのフォルダ名などを管理します。

## メソッド一覧

### BoxNode(string name, Node parent) (コンストラクタ)
名前と親ノードを指定してBOXノードを初期化します。

### LoadFromFile(string filePath, Node parent) (静的メソッド)
指定された `box.def` ファイルを解析（Shift-JIS）し、`#TITLE` や `#ARTIST` などの情報を取得して新しい `BoxNode` インスタンスを作成します。
  - `#TITLE`: フォルダの表示名として使用されます。
