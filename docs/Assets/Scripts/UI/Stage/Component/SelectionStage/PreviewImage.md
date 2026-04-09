# PreviewImage

## 役割
選曲画面において、現在選択されている楽曲やフォルダのプレビュー画像（ジャケット画像）を表示するクラスです。

## 主要なフィールド
- `mPreviewImage`: 画像を表示するためのUI `Image` コンポーネント。
- `mDefaultPreviewSprite`: 楽曲に画像が設定されていない場合や読み込み失敗時に使用するデフォルト画像。

## メソッド一覧

### OnOpen()
UIコンポーネントの取得を行い、楽曲ツリーのフォーカス変更イベントを購読します。

### OnClose()
楽曲ツリーのイベント購読を解除します。

### OnFocusNodeChanged(object sender, MusicTree.FocusNodeChangedArgs e)
フォーカスされている楽曲が変更された際に呼び出され、表示する画像を更新します。

### RefreshPreviewImage(Node node)
指定されたノードが保持しているプレビュー画像を `Image` コンポーネントにセットします。画像がない場合はデフォルトの画像を表示します。
