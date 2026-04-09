# EditorTools

## 役割
Unityエディタ上での作業を効率化するためのカスタムメニューとツールを提供する静的クラスです。テクスチャのアトラス（Sprite）化、フォント作成、ビルド準備などの補助機能が含まれます。

## メソッド一覧（メニュー項目）

### DTXMania/ShowAllEncodings
利用可能なすべての文字エンコーディングをデバッグログに出力します。

### DTXMania/DisableAllRayTargets
選択中のGameObject配下にあるすべての `Image` および `Text` コンポーネントの `Raycast Target` を一括でOFFにします。UIの最適化に使用します。

### DTXMania/AutoBuildStreamingPath
`StreamingAssets` フォルダ内の全DTXファイルを検索し、`MainScript` の `DtxFiles` 配列を自動的に更新します。ビルド時に楽曲をパッケージに含める準備に使用します。

### DTXMania/ConvertSprites
`Assets/Images` 内の `.yaml` および `.xml` 定義ファイルに基づき、対応する `.png` テクスチャを複数のSprite（スライス済み）に自動変換します。

### DTXMania/ConvertSelectYaml
現在選択しているYAMLファイルに基づいてスプライト変換を実行します。

### DTXMania/BuildSelectAsFont
選択中のテクスチャと、そのSpriteスライス情報から、Unityの `Font` アセットを生成します。

### DTXMania/TryDecodeXaAudio
`StreamingAssets` フォルダ内の `.xa` ファイルをテストデコードし、シーン内の `AudioSource` で再生確認を行います。

## 内部補助メソッド

### ConvertYaml / ConvertXml
特定のフォーマット（YAML/XML）で記述された矩形情報を解析し、Spriteスライス用のデータ構造（`SpriteRect`）に変換します。

### ConvertSprite
解析した矩形情報を元に、Unityの `TextureImporter` を介して実際にテクスチャを Multiple Sprite モードに設定・適用します。座標系の変換（上基準から下基準へ）も行います。
