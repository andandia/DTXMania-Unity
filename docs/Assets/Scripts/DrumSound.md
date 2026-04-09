# DrumSound

## 役割
ドラムの各パーツの打撃音（AudioClip）を管理し、再生する機能を提供します。チップの種類に応じて適切な音を鳴らし、必要に応じて特定のグループ（シンバルやハイハットなど）の音を消音（ミュート）する機能も持ちます。

## 主要なフィールド
- `DrumClips`: 再生可能なドラム音の`AudioClip`の配列。インスペクターから設定します。ファイル名は`ChipType`の名称と一致している必要があります。
- `mChipTypeToDrumSound`: `ChipType`をキーとし、対応する音の情報を保持する`DrumSoundInfo`を値とする辞書。

## メソッド一覧

### Start()
起動時に呼び出され、`DrumClips`に設定された各オーディオクリップに対して`DrumSoundInfo`を作成し、内部の辞書を初期化します。各クリップ用にGameObjectが作成され、複数の`AudioSource`がアタッチされます。

### PlaySound(ChipType chipType, bool muteOther = false, MuteGroupType muteGroup = MuteGroupType.Unknown, float volume = 1f)
指定された`ChipType`の音を再生します。
- `muteOther`: 真の場合、同じ`muteGroup`に属する他の音を停止してから再生します。
- `muteGroup`: 消音グループを指定します。
- `volume`: 再生音量を指定します。

## 内部クラス: DrumSoundInfo

### 役割
一つのドラムパーツに関する音の情報を管理します。同じ音が重なって聞こえるように、複数の`AudioSource`（デフォルトで4つ）を持ち、順繰りに使用（ラウンドロビン）します。

### メソッド
- `PlaySound(MuteGroupType muteGroup, float volume)`: 空いている`AudioSource`を使用して音を再生します。
- `Stop()`: このパーツに属するすべての`AudioSource`の再生を停止します。
