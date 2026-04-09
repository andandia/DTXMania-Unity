# ChipDrawingList

## 役割
演奏中に画面上に描画すべきノーツ（チップ）のリストを管理し、それらの描画位置を計算するクラスです。現在の演奏時間とスクロール速度に基づいて、画面内にあるチップを抽出します。

## 主要なフィールド
- `mPlayingStage`: 演奏画面のメインクラスへの参照。
- `mStartDrawNumber`: 描画を開始するチップのインデックス。画面外に消えたチップをスキップするために保持されます。
- `hitJudgPosY`: 判定ラインの座標値（チップがこれを超えると画面外とみなされます）。

## メソッド一覧

### ChipDrawingList(PlayingStage playingStage) (コンストラクタ)
演奏ステージの参照を保持してリストを初期化します。

### Update(Score score, float playingTime, float speed)
毎フレーム呼び出され、描画対象となるチップを再計算します。
- スコア内の全チップのうち、現在の時間から計算して画面内に入っているものを抽出します。
- 各チップの描画時間、発音時間、および判定ラインからの距離（ピクセル単位）を計算し、`ChipDrawingInfo`としてリストに追加します。
- すべてのチップの処理が終了した場合、`mPlayingStage.OnPlayintFinished()`を呼び出します。

### GetPixleDistanceOnTime(float speed, float time)
時間差とスクロール速度から、判定ラインからの移動距離（ピクセル）を計算します。内部で定義された定数により、標準のスクロール速度が調整されています。

## 構造体: ChipDrawingInfo
個々のチップの描画情報を保持します。
- `Chip`: オリジナルのチップデータ。
- `DrawingTime`: 描画基準時間からの経過時間。
- `UtterTime`: 発音基準時間からの経過時間。
- `PixelDistance`: 判定ラインからの距離。
