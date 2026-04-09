# SetDef

## 役割
`set.def` ファイルを解析するためのデータ構造とメソッドを定義するクラスです。`set.def` は一つの楽曲タイトルに対して、複数の難易度ファイル（L1〜L5）を紐付けるために使用されます。

## 内部クラス: Block
`set.def` 内の各楽曲ブロック（タイトル、難易度別ファイル名、ラベル、ジャンル、フォント色など）を保持します。

## メソッド一覧

### RestoreFrom(string filePath) (静的メソッド)
指定された `set.def` ファイルを読み込み（Shift-JIS）、`Block` オブジェクトのリストとして解析結果を返します。
- `#TITLE` が見つかるたびに新しいブロックとして開始します。
- `L1FILE` 〜 `L5FILE` および `L1LABEL` 〜 `L5LABEL` を解析して難易度情報を構築します。

### FixBlockLevelLabels(Block block) (静的メソッド)
ブロック内の各難易度に対して、明示的なラベルが指定されていない場合にデフォルトの名称（BASIC, ADVANCED, EXTREME, MASTER, ULTIMATE）を割り当てます。
