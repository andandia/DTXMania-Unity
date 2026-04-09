# Stage

## 役割
ゲーム内の各画面（タイトル、選曲、演奏など）を表す抽象的な基底クラスです。`Activity`を継承しており、`StageManager`によって管理されます。

## メソッド一覧

### Stage() (コンストラクタ)
基底クラスのコンストラクタを呼び出して初期化します。

### Close()
`StageManager.Instance.Close(this)`を呼び出し、自身を閉じます。
