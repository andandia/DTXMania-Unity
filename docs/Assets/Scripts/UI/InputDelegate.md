# InputDelegate

## 役割
UnityのUIボタンなどからのイベントを受け取り、それをドラム入力イベントとして`InputManager`に橋渡しするコンポーネントです。タッチパネル操作などをサポートするために使用されます。

## メソッド一覧

### OnOK()
決定操作に対応するドラム入力（左シンバル）を発生させます。

### OnMoveUp()
上移動に対応するドラム入力（ハイタム）を発生させます。

### OnMoveDown()
下移動に対応するドラム入力（ロータム）を発生させます。

### ManualyInputKey(DrumInputType drumInputType)
指定されたドラム入力タイプを`InputManager`のキューに直接追加します。
