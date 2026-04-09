# MidiDebug

## 役割
MIDI入力をデバッグ表示するための補助クラスです。受信したMIDIメッセージの生情報をUI上に表示します。

## メソッド一覧

### Start()
`UsbMidiDriver` の `OnMidiNoteOn` イベントに、デバッグテキスト表示用メソッドを登録します。

### Update()
デバッグ用UIを常に最前面に表示し続けるよう、UIヒエラルキーの順序を調整します。

### ShowDebugText(string text)
受け取ったMIDIメッセージ文字列を `Text` コンポーネントに反映させます。
