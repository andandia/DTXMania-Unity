# Activity

## 役割
UI要素やステージなどの階層構造を持つコンポーネントの基底クラスです。UnityのGameObjectと紐付けられ、親子関係の管理、更新、コルーチンの開始、コンポーネントの検索などの基本機能を提供します。

## 主要なフィールド
- `GameObject`: 紐付けられているUnityのGameObject。
- `Transform`: 紐付けられているGameObjectのTransform。
- `Parent`: 親の`Activity`。
- `ChildList`: 子の`Activity`のリスト。

## メソッド一覧

### Activity(GameObject go) (コンストラクタ)
GameObjectを指定して初期化します。

### OnOpen() / OnClose() / Update() (仮想メソッド)
オープン時、クローズ時、および毎フレームの更新時に呼び出されます。これらのメソッドは子Activityに対しても再帰的に呼び出されます。

### StartCoroutine(IEnumerator corutin)
`MainScript`のインスタンスを経由してコルーチンを開始します。

### FindChild(string path)
紐付けられたGameObjectから指定したパスの子要素を検索します。

### GetComponent<T>(string path)
指定したパスの子要素から特定のコンポーネントを取得します。

### AddChild<T>(T childActivity, bool fireOnOpen = true)
子Activityを追加し、必要に応じてその`OnOpen`を呼び出します。

### RemoveChild<T>(T childActivity, bool fireOnClose = false)
子Activityを削除し、必要に応じてその`OnClose`を呼び出します。
