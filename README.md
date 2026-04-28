# almand — 포트폴리오

게임 클라이언트 개발 포트폴리오입니다.
Unity(C#) 기반 모바일/WebGL 게임 개발 경험과 플랫폼 SDK 추상화 설계 코드를 담고 있습니다.

> 각 폴더는 개발물/사이드 프로젝트의 **부분 코드**입니다.
> 회사 코드(서버, IAP, 광고 SDK 등)는 라이선스 사유로 제외되어 있어, README에는 **이 저장소에 실제로 업로드된 코드 기준**으로 작성했습니다.

---

## 프로젝트 구성

### android/SampleCollectibleRPG
> 수집형 RPG — UI 시스템 부분 코드 (로그인 / 장비 / 다이얼로그)

- **사용 기술**: Unity, C#, UGUI(TextMeshPro), DOTween, PlayMaker(FSM), LuaInterface, XAsset
- **핵심 구현**
  - `BaseView` 상속 기반 UI 계층 구조 (`EquipmentsBaseUIView` → `EquipmentsHandBookUIView` 등)
  - 장비 핸드북/리스트/상세 UI를 공통 베이스 + 탭 패널로 구성 (`Equips/` 6개 View)
  - 로그인 플로우 9종: 서버 선택, 계정 입력, 공지사항, CDK 입력, 가짜 로딩 슬라이더, 연결 끊김 처리 등
  - `DialogUIView` — 시리즈 ID 기반 컷씬 대사 시스템 (캐릭터 모델/위치/순서 데이터 주도)
  - `XAsset.Assets.LoadWithOwner<Sprite>` 기반 비동기 에셋 로딩

### android/SampleIdleRPG
> 방치형 RPG — 전투 / 아바타 / 매니저 부분 코드

- **사용 기술**: Unity, C#, NGUI, DOTween, LitJson, SharpZipLib, Socket 통신
- **핵심 구현**
  - 라운드 기반 전투 재생 시스템 — 배속(1.2x/2x/3x), 스킵, 리플레이, 일시정지 (`Battle.cs`)
  - 스프라이트 아틀라스 기반 2D 아바타 상태 머신 — 전투용/도시용/감정 표현(Face enum 11종)
  - `Socket.Send(CMD.Role_login, …)` 패킷 기반 서버 통신, `LitJson` 직렬화
  - `IniFile` 기반 다국어 시스템 (`Manager.Language.GetString`)
  - `ResourceManager` — AssetBundle + GZip 압축 캐시 시스템

### webgl/SimpleChatBot
> ChatGPT API 활용 챗봇 — Unity WebGL 빌드

- **사용 기술**: Unity, C#, OpenAI API, DeepL API, WebGLSupport(한글 입력)
- **핵심 구현**
  - `SAMKDS.cs` — OpenAI Chat Completions(`/v1/chat/completions`) 연동, `gpt-3.5-turbo` 사용
  - 대화 히스토리 관리(`m_strHistories`) + 토큰 절약용 요약 메시지 빌더(`makeSummarizeMessage`)
  - `TranslateManager` — DeepL API로 한국어 입력 → 영어 자동 번역 후 GPT 전달
  - `MapManager` / `Map.cs` — 타일 기반 맵 시스템
  - 캐릭터 Animator + `VideoPlayer`(StreamingAssets mp4) 연출
  - WebGL 빌드 산출물 (`WEBsam_chatGPT/`) 포함

### unity_vibe_claude
> Vampire Survivors류 게임 — Claude Code 바이브 코딩으로 구조 설계

- **사용 기술**: Unity (URP 2D), C#, TextMeshPro
- **핵심 구현**
  - 게임 플로우 (`GameFlowUI`) — 타이틀 → 플레이 → 게임오버 → Retry, `Time.timeScale` 제어
  - `EventManager` — 문자열 키 기반 중앙 Pub/Sub (시스템 간 직접 참조 제거)
  - `ObjectPoolManager` + `IPoolable` 인터페이스 — 제네릭 풀
  - `AutoShootWeapon` — 가장 가까운 적 탐색 후 자동 발사 (사거리/공속/데미지 모디파이어)
  - `EnemySpawner` — 시간 기반 난이도 스케일링 (소환 간격 감소 + HP 증가, `_elapsedTime` 누적)
  - 경험치/레벨업 (`ExpSystem`) → `OnLevelUp` 이벤트로 `SkillManager`가 랜덤 스킬 3개 제시 (`SkillChoiceUI`)
  - `Singleton<T>` 제네릭 베이스

### Platform
> 플랫폼 SDK 추상화 레이어 — Auth / IAP / Firebase **아키텍처 설계**

> ℹ️ 이 모듈은 **인터페이스 + Strategy 패턴 설계 + Editor Mock 구현**이 핵심입니다.
> Google/Apple/ONE Store 실제 SDK 호출부는 빌드 환경 분리를 위해 `// TODO`로 남겨 두었습니다 (각 SDK 의존성 미포함 상태에서 컴파일 가능하도록).

- **사용 기술**: Unity, C#, Strategy + Service Locator 패턴, scripting define 기반 분기
- **핵심 구현**
  - `IAuthProvider` 인터페이스 → `EditorAuthProvider`(Mock 동작) / `GoogleAuthProvider` / `AppleAuthProvider` / `OneStoreAuthProvider`(스켈레톤)
  - `IIAPProvider` 인터페이스 → 동일 구조 4개 구현체
  - `PlatformServiceLocator` — `UNITY_EDITOR / UNITY_IOS / UNITY_ANDROID + ONE_STORE` 디파인으로 빌드 타겟 자동 감지 후 Provider 주입
  - `EditorAuthProvider` / `EditorIAPProvider` — 실제 SDK 없이 로그인·구매 플로우 테스트 가능 (Mock 결제 데이터, 트랜잭션 카운터 등)
  - `FirebaseAuthBridge` — 플랫폼 credential → Firebase Auth 연동을 위한 브릿지 구조 (실제 Firebase SDK 호출부는 주석으로 가이드 제시)

### python
> 기획 데이터 변환 도구

- **사용 기술**: Python 3, openpyxl
- **핵심 구현**
  - Excel(.xlsx) → CSV 변환 (`convertCSV.py`)
  - 빈 헤더 자동 감지로 유효 컬럼 범위 결정
  - 가변인자(`*excludedCols`)로 특정 열 제외 지원
  - UTF-8-BOM(`utf-8-sig`) 출력으로 Excel 한글 호환

---

## 기술 스택

| 분류 | 기술 |
|------|------|
| **언어** | C#, Python |
| **엔진** | Unity (Android, iOS, WebGL, URP 2D) |
| **UI** | UGUI / TextMeshPro / NGUI |
| **아키텍처 패턴** | MVC(BaseView), Singleton, Strategy, Service Locator, Pub/Sub, Object Pool |
| **외부 연동 / API** | OpenAI Chat Completions, DeepL Translate, Firebase Auth(브릿지), Google Play Games / Apple Sign-In / ONE Store(인터페이스 추상화) |
| **라이브러리** | DOTween, PlayMaker, LuaInterface, XAsset, LitJson, SharpZipLib, WebGLSupport |
| **네트워크** | Socket(TCP), UnityWebRequest, WWW |
