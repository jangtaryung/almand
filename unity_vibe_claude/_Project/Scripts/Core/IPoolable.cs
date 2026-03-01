/// <summary>
/// 오브젝트 풀에서 관리되는 오브젝트가 구현하는 인터페이스.
/// 풀에서 꺼낼 때와 반환할 때 자동으로 호출됩니다.
/// </summary>
public interface IPoolable
{
    /// <summary>
    /// 풀에서 꺼내서 활성화될 때 호출. 초기화 로직 작성.
    /// </summary>
    void OnGetFromPool();

    /// <summary>
    /// 풀에 반환될 때 호출. 정리 로직 작성.
    /// </summary>
    void OnReleaseToPool();
}