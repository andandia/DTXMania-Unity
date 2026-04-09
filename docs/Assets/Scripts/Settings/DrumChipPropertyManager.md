# DrumChipPropertyManager

## 役割
ドラムの各チップ（ノーツ）の種類ごとに、表示方法や判定方法、オートプレイ時の挙動などの特性（`DrumChipProperty`）を管理するクラスです。プレイモード（BASIC/EXPERT）や楽器の左右配置設定に応じて、これらの特性を動的に更新します。

## 主要なフィールド
- `mplayMode`: 現在のプレイモード（BASIC/EXPERT）。
- `mDisplaySide`: ライドやチャイナなどのシンバル類を左右どちらに表示するかの設定。
- `mInputPresetType`: 入力のプリセットタイプ（シンバルフリーなど）。
- `ChipToProperty`: `ChipType`をキーとし、対応する`DrumChipProperty`を値とする辞書。
- `this[ChipType chipType]`: 指定したチップタイプのプロパティにアクセスするためのインデクサ。

## メソッド一覧

### DrumChipPropertyManager(PlayMode playMode, LeftAndRightDisplayTrack displaySide, InputPresetType presetType) (コンストラクタ)
初期設定を受け取り、すべての`ChipType`に対してデフォルトのプロパティを生成・登録します。その後、現在のモード設定を反映させます。

### 反映する(PlayMode mode)
プレイモード（BASICまたはEXPERT）の変更を各チップのプロパティに反映させます。
- 例: EXPERTモードではハイハットのオープン/クローズを個別のチップとして表示し、BASICモードでは共通のハイハットチップとして表示するように設定を書き換えます。

### 反映する(LeftAndRightDisplayTrack position)
シンバル類の左右表示設定の変更を反映させます。
- ライド、チャイナ、スプラッシュの表示レーン、オートプレイ対象、消音グループなどを更新します。

### 反映する(InputPresetType preset)
入力プリセットの変更を反映させます。
- 「シンバルフリー」設定の場合、複数のシンバル入力を一つの「Cymbal」グループとして扱うように設定します。
