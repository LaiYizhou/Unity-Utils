# Unity-Utils

A micro collection of unity3d utils for me and by me.

---

- [**DataStructures**](https://github.com/LaiYizhou/Unity-Utils/tree/master/DataStructures)

  It contains heap

  Note:

  ```c#
  MinHeap<int[]> minHeap 
  	= new MinHeap<int[]>(Comparer<int[]>.Create((a, b) => { return a[0] - b[0];}));
  ```

  it can work in .NET 4.8 but *can't work in some Unity Editor*

- [**TimeUtil**](https://github.com/LaiYizhou/Unity-Utils/tree/master/TimeUtil)

  It can check daily function, such as daily login, daily bonus, daily puzzle

  eg:

  ```c#
  int CheckDaily(DateTime now, DateTime last)
  ```

  ```c#
  bool IsNewDay(DateTime now, DateTime last)
  ```

  ​    

- [**BitUtil**](https://github.com/LaiYizhou/Unity-Utils/tree/master/BitUtil)

  It's about bit operation

  eg:

  ```c#
  int SetBit(this int A, int k, bool val)
  ```

  ```c#
  int ToggleBit(this int A, int k)
  ```

  ```c#
  bool GetBit(this int A, int k)
  ```

  ```c#
  string ToBinaryString(this int A, char sep = ',')
  ```

​    

- [**LINQUtil**](https://github.com/LaiYizhou/Unity-Utils/tree/master/LINQUtil)

  It's extension based on LINQ

  eg:

  ```c#
  string ToOneLineString()
  ```

  ```c#
  T RandomOne<T>()
  ```

  ```c#
  T RandomSome<T>()
  ```

​    

- [**EditorButton**](https://github.com/LaiYizhou/Unity-Utils/tree/master/EditorButton)

  It can add buttons in Inspector panel in Unity3D with attribute.

  eg:

  ```c#
  public class Test : MonoBehaviour
  {
      [Button]
      public void PrintName()
      {
          Debug.Log(name);
      }
  }
  ```
  ![20190710172035](_/20190710172035.jpg)

​    

- [**EditorRename**](https://github.com/LaiYizhou/Unity-Utils/tree/master/EditorRename)

  It can rename the field which showed in Inspector panel.

  eg:

  ```c#
  public enum EColor
  {
      [Rename("红色")]
      A,
      [Rename("黄色")]
      B,
      [Rename("蓝色")]
      C
  }
  
  public class Test : MonoBehaviour
  {
      [Rename("年龄")]
      public int Age;
  
      [Rename("颜色")]
      [SerializeField]
      private EColor color;
  }
  ```

  ![20190710172255](_/20190710172255.jpg)

