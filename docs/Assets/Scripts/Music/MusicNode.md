# MusicNode

## 役割
単一の楽曲ファイル（DTX等）を表すノードクラスです。`SSTFormat.v4.Score` を保持し、楽曲のメタデータ（タイトル、アーティスト、難易度、プレビュー情報）を管理します。

## 主要なフィールド
- `MusicPath`: 楽曲ファイルのフルパス。
- `Score`: 解析済みの楽曲スコアデータ。

## メソッド一覧

### MusicNode(string musicPath, Node parent, Stream stream = null) (コンストラクタ)
楽曲ファイルのパスと親ノードを受け取り、楽曲データの読み込みを開始します。

### LoadSongData(Stream stream = null)
ファイルを解析して `Score` オブジェクトを生成し、そこからタイトル、アーティスト名、プレビュー画像・音声のパスを抽出して自身に設定します。
  - デフォルトで難易度レベル3（FREE）に値を設定します。
