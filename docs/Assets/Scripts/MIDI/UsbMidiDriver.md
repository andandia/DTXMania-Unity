# UsbMidiDriver

## 役割
Androidデバイスに接続されたUSB MIDIデバイスからの入力を処理するためのクラスです。MIDIネイティブライブラリからのコールバック（NoteOn/NoteOff）を受け取り、ゲーム内で利用可能な形式（ボタンの押し下げ判定など）に変換します。

## 主要なフィールド
- `mMidiNoteInput`: 各MIDIノート番号の現在の入力状態（前フレームと現フレームのON/OFF）を保持する辞書。
- `OnMidiNoteOn`: MIDIのNoteOnイベントが発生した際に呼び出される外部用アクション。

## メソッド一覧

### LateUpdate()
全MIDIノートの「前フレームの状態」を現在の状態で更新し、次フレームの入力判定に備えます。

### onMidiNoteOn(string noteInfo) / onMidiNoteOff(string noteInfo)
MIDIライブラリから呼び出されるコールバックメソッドです。カンマ区切りの文字列情報（デバイスアドレス、チャンネル、ノート番号、ベロシティなど）を受け取り、ノート番号を解析して状態を更新します。

### OnMidiNoteChanged(int midiNote, bool onOff)
指定されたMIDIノート番号の現在の状態を `mMidiNoteInput` 辞書に反映させます。

### GetMidiNoteOn(int midiNote)
指定されたMIDIノートが「このフレームで新しく押されたか」を返します。
- 前フレームがOFFかつ、現フレームがONの場合にのみ真となります。
