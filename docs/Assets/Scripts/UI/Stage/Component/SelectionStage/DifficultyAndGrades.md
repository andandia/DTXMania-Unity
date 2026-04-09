# DifficultyAndGrades

## 役割
選曲画面において、現在選択されている楽曲の難易度レベル（各レベルごとの数値）を表示するクラスです。

## メソッド一覧

### OnOpen()
楽曲ツリーのフォーカス変更イベントを購読し、初期表示の更新（`RefreshFocusNodeInfo`）を行います。

### RefreshFocusNodeInfo()
現在フォーカスされている楽曲ノードの難易度情報を読み取り、UI上の「Levels」パネル内の各項目に反映させます。

### SetupDiffNode(Transform node, Node.DifficultyLabel difficultyLabel, bool selected)
個々の難易度表示（レベル名、整数値、小数点以下の値）をセットアップします。現在選択されている難易度レベルの場合は、「Selected」演出を有効にします。
