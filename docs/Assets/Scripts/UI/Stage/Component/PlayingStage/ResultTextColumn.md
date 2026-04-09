# ResultTextColumn

## 役割
ノーツをヒットした際に、そのトラック（レーン）の判定（PERFECT, GREATなど）を文字アニメーションとして表示するクラスです。

## 主要なフィールド
- `mTrackDrumMap`: トラックの種類（`DisplayTrackType`）と、対応する判定文字アニメーションコンポーネントの辞書。

## メソッド一覧

### ResultTextColumn(GameObject gameObject) (コンストラクタ)
初期化時に各子要素を走査し、対応するトラックの判定文字アニメーションを登録します。

### OnHit(DisplayTrackType displayTrackType, JudgmentType judgmentType)
指定されたトラックでヒット（またはミス）が発生した際に呼び出されます。判定結果（`judgmentType`）の名前に対応するアニメーション（例: "PERFECT" という名前のアニメーション）を再生します。
  - 同一トラックで連続して判定が発生した場合は、前のアニメーションを止めて最初から再生し直します。
