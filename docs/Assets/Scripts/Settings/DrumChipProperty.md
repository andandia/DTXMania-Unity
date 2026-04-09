# DrumChipProperty

## 役割
特定のドラムチップ（ノーツ）の種類に関する詳細な挙動定義を保持するデータクラスです。表示トラック、使用する画像タイプ、入力タイプ、オートプレイ時の挙動フラグなどが含まれます。

## 主要なフィールド
- `ChipType`: SSTFormatで定義されているチップの元々の種類。
- `DrumType`: 楽器の種類。
- `DisplayTrackType`: どのトラック（レーン）に表示するか。
- `DisplayChipType`: どの画像（チップデザイン）を使用して表示するか。
- `DrumInputType`: どのドラム入力に対応するか。
- `AutoPlayType`: どのオートプレイ設定に従うか。
- `InputGroupType`: 入力のグループ化設定。
- `MuteBeforeUtter`: 発音前に同じグループの音を消音するかどうか。
- `MuteGroupType`: どの消音グループ（ハイハット、シンバルなど）に属するか。

### オートプレイON時の挙動フラグ
- `AutoPlayON_AutoHitSound`: 自動で音を鳴らすか.
- `AutoPlayON_AutoHitHide`: 自動ヒット時にチップを消すか。
- `AutoPlayON_AutoJudge`: 自動で判定（PERFECTなど）を発生させるか。
- `AutoPlayON_MissJudge`: 空振り時にMISS判定を出すか。

### オートプレイOFF時の挙動フラグ
- `AutoPlayOFF_AutoHitSound / Hide / Judge`: オートプレイOFFでも自動でヒットさせる場合の挙動（通常はfalse）。
- `AutoPlayOFF_UserHitSound / Hide / Judge`: ユーザーが叩いた時の発音、消去、判定の有無。
- `AutoPlayOFF_MissJudge`: 見逃し時にMISS判定を出すか。
