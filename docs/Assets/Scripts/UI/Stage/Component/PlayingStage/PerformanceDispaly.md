# PerformanceDispaly

## 役割
演奏画面やリザルト画面において、判定ごとのヒット数（PERFECTの数など）とその割合（％）、および最大コンボ数をリアルタイムに表示するクラスです。

## 主要なフィールド
- `mCurrentGrade`: 表示する成績データを保持する `Grade` クラス。
- `mJudgmentNode`: 判定タイプごとの表示UI（カウント用テキストと％用テキスト）を保持する辞書。
- `mMaxComboNode`: 最大コンボ数を表示するためのUI情報。

## メソッド一覧

### OnOpen()
UI内の各判定項目（PERFECT, GREATなど）に対応するテキストコンポーネントを検索し、辞書に登録します。初期状態での更新も行います。

### Update()
`Grade` クラスのデータが更新された（GradeDirtyが真）場合に、数値を再計算して表示を更新します。

### UpdateText(Grade grade)
`Grade` クラスから最新のヒット数と割合、最大コンボ数を取得し、各表示ノード（`DrawTypeNode`）に反映させます。

### DrawTypeNode(TypeNodeInfo typeNode, int hitNumber, int hitPercentage)
特定の項目の数値とパーセンテージをテキストとして描画します。

### BuildDrawNumber(int drawNumber, int digist, float opacity = 1.0f)
数値を指定された桁数にフォーマットします。
- 桁数に満たない部分は 'o' 文字（UIフォント上でグレーの0として表現される）で埋めます。
- 指定された桁数を超える場合は、その桁数での最大値（999など）でカンストさせます。
