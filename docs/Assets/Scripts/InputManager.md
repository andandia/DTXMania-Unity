# InputManager

## 役割
キーボードやMIDIデバイスからの入力を管理し、ゲーム内のアクション（決定、キャンセル、ドラム入力など）に変換するシングルトンクラスです。

## 主要なフィールド
- `Instance`: `InputManager`の唯一のインスタンスにアクセスするための静的プロパティ。
- `mKeyBindings`: キーボードやMIDIのノート番号とドラム入力タイプの対応付けを保持するオブジェクト。
- `mDrumInputEvents`: 現在のフレームで発生したドラム入力イベントのリスト。
- `EnableDrumKeyChecking`: キーボードによるドラム入力のチェックを有効にするかどうかのフラグ。

## メソッド一覧

### HasOk(bool checkDrumInput = true)
決定操作（Submitボタンまたは特定のドラム入力）が行われたかを確認します。
`checkDrumInput`が真の場合、クラッシュシンバルやライドシンバルなどの入力も決定操作として扱います。

### HasCancle(bool checkDrumInput = true)
キャンセル操作（Cancelボタンまたは特定のドラム入力）が行われたかを確認します。
`checkDrumInput`が真の場合、フロアタム（Tom3）の入力もキャンセル操作として扱います。

### HasMoveUp(bool checkDrumInput = true) / HasMoveDown(bool checkDrumInput = true)
上下の移動操作が行われたかを確認します。ドラム入力ではハイタム（Tom1）が上、ロータム（Tom2）が下に割り当てられています。

### HasMoveRight(bool checkDrumInput = true) / HasMoveLeft(bool checkDrumInput = true)
左右の移動操作が行われたかを確認します。ドラム入力ではフロアタム（Tom3）が右、スネア（Snare）が左に割り当てられています。

### CheckingInput()
現在入力を受け付けるべき状態かどうかを判定します。画面遷移中（SwitchManagerにアクティブなスイッチがある場合）はfalseを返します。

### Update()
毎フレーム呼び出され、入力デバイスの状態をポーリングします。

### PollAllInputDevices()
すべての入力デバイス（キーボード、MIDI）をチェックし、発生した入力を`mDrumInputEvents`に格納します。
前フレームのイベントはクリアされ、オブジェクトプールに戻されます。

### HasAnyDrumInput(params DrumInputType[] drumInputTypes)
指定されたドラム入力タイプのいずれかが発生しているかを確認します。
入力があった場合、そのイベントは「処理済み（Processed）」としてマークされます。

### EnqueueDrumInputEvent(DrumInputEvent drumInputEvent)
外部からドラム入力イベントを直接キューに追加します。

### GetDrumInput(DrumInputType drumInputType)
特定のドラム入力タイプが発生しているかを確認します。
入力があった場合、そのイベントは「処理済み（Processed）」としてマークされます。
