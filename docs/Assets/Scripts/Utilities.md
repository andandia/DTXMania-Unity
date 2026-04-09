# Utilities

## 役割
プロジェクト全体で利用される便利な補助機能を提供する静的クラスです。

## メソッド一覧

### ParseLineParams(string line, string cmd, out string parameter)
DTXファイルなどのテキスト行から、指定されたコマンド（例: `#TITLE`）に続くパラメータを抽出します。
- セミコロンによるコメントを無視します。
- 正規表現を使用して、コマンドと値のペアを解析します。

### GetComponent<T>(this Transform transform, string path) (拡張メソッド)
`Transform`型の拡張メソッドです。指定された相対パスにある子要素からコンポーネントを取得します。
Unity標準の`transform.Find(path).GetComponent<T>()`を簡略化したものです。
