# SelectableList<T>

## 役割
リスト構造に「現在選択されている要素」という概念を追加した汎用クラスです。`List<T>`を継承しており、要素の選択状態が変わった際にイベントを通知する機能を備えています。

## 主要なフィールド
- `SelectedIndex`: 現在選択されている要素のインデックス。
- `SelectedItem`: 現在選択されている要素のオブジェクト。
- `SelectionChanged`: 選択要素が変更された際に発行されるイベント。

## メソッド一覧

### SelectItem(int index)
指定されたインデックスの要素を選択します。範囲外の場合はfalseを返します。

### SelectItem(T element)
指定されたオブジェクトがリスト内にあれば、それを選択します。

### SelectItem(System.Func<T, bool> selector)
ラムダ式などの条件に一致する最初の要素を選択します。

### OnSelectedChanged(int preIndex)
内部的な選択状態の変更を処理し、`SelectionChanged`イベントを呼び出します。新しく選択されたアイテムと、直前まで選択されていたアイテムが通知されます。

### SelectFirst() / SelectLast()
リストの最初、または最後の要素を選択します。

### SelectNext(bool Loop = false)
次の要素を選択します。`Loop`が真の場合、末尾の次は先頭に戻ります。

### SelectPrev(bool Loop = false)
前の要素を選択します。`Loop`が真の場合、先頭の次は末尾に移動します。
