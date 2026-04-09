# WAVManager

## 役割
DTX曲データに含まれるWAV/MP3/XAなどの音声ファイルを管理し、演奏中に再生する機能を提供します。BGMや各チップの固有音（WAV）の読み込み、キャッシュ、再生を制御します。

## 主要なフィールド
- `AudioSource`: 音声再生に使用するメインの`AudioSource`。
- `mWavInfoList`: WAV ID（DTX内での番号）と音声情報（`WaveInfo`）の辞書。
- `mWaveCacheList`: ファイルパスと読み込み済み`AudioClip`のキャッシュ辞書。

## メソッド一覧

### Start()
コンポーネントの初期化時に`AudioSource`を確保します。

### Clear()
登録されているすべての音声データと対応するGameObjectを破棄し、リストをクリアします。曲の切り替え時などに使用されます。

### PlaySound(string soundFilePath, bool loop = false)
ファイルパスを直接指定して音声を再生します。主にプレビュー音やBGM用です。キャッシュにない場合は非同期で読み込みを行います。

### Stop()
メイン`AudioSource`の再生を停止します。

### Sinup(int wavId, WWW clipWWW, bool loop)
DTX解析時に呼び出され、WAV IDと音声データを紐付けます。`.xa`形式の場合は独自のデコーダー（bjxa）を使用して読み込みます。

### PlaySound(int wavId, ChipType chipType, bool muteOther, MuteGroupType muteGroup, float volume = 1.0f, float delayTime = 0.0f)
WAV IDを指定して音声を再生します。演奏中にノーツがヒットした際などに呼び出されます。
- 指定されたWAV IDが登録されていない場合は、デフォルトのドラム音（`DrumSound`）で代用します。
- 消音グループの設定に基づき、必要に応じて他の音を停止します。

### GetAudioClipFromWWW(WWW clipWWW)
`WWW`オブジェクトから`AudioClip`を取り出します。拡張子を判別し、WAV/MP3/OGG/XAの各形式に対応します。

### DecodeXaAudioData(string name, byte[] data)
独自のXA音声データをデコードして`AudioClip`を作成します。

## 内部クラス: WaveInfo
特定のWAV IDに関連付けられた音声再生オブジェクトを管理します。`DrumSoundInfo`と同様に、複数の`AudioSource`を持ち、音が重なっても途切れないように再生（ラウンドロビン）します。
