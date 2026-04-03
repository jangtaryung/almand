# almand — 포트폴리오

게임 클라이언트 개발 포트폴리오입니다.  
Unity(C#) 기반 모바일/WebGL 게임 개발과 플랫폼 SDK 설계 역량을 담고 있습니다.

---

## 프로젝트 구성

### android/SampleCollectibleRPG
> 수집형 RPG — UI 시스템 부분 코드

- **사용 기술**: Unity, C#, NGUI/UGUI, DOTween, PlayMaker(FSM), LuaInterface, XAsset
- **핵심 구현**
  - MVC 기반 UI 아키텍처 (`BaseView` 상속 구조)
  - 장비 강화/승급/성급업 탭 UI (`EquipmentsUIView` 계열)
  - 서버 선택, 계정 입력, 공지사항 등 로그인 플로우
  - Lua 연동 대화 연출 시스템, 이벤트 기반 통신, 비동기 에셋 로딩

### android/SampleIdleRPG
> 방치형 RPG — 전투/아바타/매니저 부분 코드

- **사용 기술**: Unity, C#, NGUI, DOTween, LitJson, 소켓 통신
- **핵심 구현**
  - 서버 권위 기반 턴/라운드 전투 재생 시스템 (배속/스킵/리플레이)
  - 스프라이트 아틀라스 기반 아바타 상태 머신 (전투/도시/감정 표현)
  - 다중 스토어 결제(Google/OneStore/iOS), IronSource 광고, 푸시 알림 통합
  - 소켓 기반 서버 통신, 다국어 지원

### webgl/SimpleChatBot
> ChatGPT API 활용 챗봇 — WebGL 빌드

- **사용 기술**: Unity, C#, OpenAI API(GPT-3.5-turbo), DeepL API, WebGL
- **핵심 구현**
  - OpenAI Chat Completions API 연동 및 대화 히스토리 관리
  - 토큰 절약을 위한 자동 대화 요약 전략
  - 한국어 입력 → DeepL 영어 번역 파이프라인
  - 3D 아바타 + VideoPlayer 연출, 타일 기반 맵 시스템

### unity_vibe_claude
> Vampire Survivors류 뱀서라이크 게임 — Claude Code 바이브 코딩으로 제작

- **사용 기술**: Unity, C#, 2D 물리, TextMeshPro
- **핵심 구현**
  - 완성형 게임 루프 (타이틀 → 플레이 → 게임오버 → 리트라이)
  - EventManager 기반 시스템 간 디커플링
  - 제네릭 오브젝트 풀 (`IPoolable` 인터페이스)
  - 자동 조준 무기, 시간 기반 난이도 스케일링, 레벨업 스킬 선택 시스템

### Platform
> 멀티 플랫폼 SDK 레이어 — Auth / IAP / Firebase

- **사용 기술**: Unity, C#, Strategy + Facade + Service Locator 패턴
- **핵심 구현**
  - `IAuthProvider` 인터페이스 → Google/Apple/OneStore/Editor 4개 구현체
  - `IIAPProvider` 인터페이스 → Google Play Billing/Apple StoreKit/OneStore IAP/Editor Mock
  - `PlatformServiceLocator`가 빌드 타겟별 자동 감지 및 Provider 주입
  - `FirebaseAuthBridge`로 플랫폼 로그인 → Firebase Auth credential 연동
  - Editor Mock Provider로 SDK 없이 테스트 가능

### python
> 기획 데이터 변환 도구

- **사용 기술**: Python 3, openpyxl
- **핵심 구현**
  - Excel(.xlsx) → CSV 자동 변환. 열 제외/범위 자동 감지, UTF-8-BOM 출력

---

## 기술 스택

| 분류 | 기술 |
|------|------|
| **언어** | C#, Python, JavaScript |
| **엔진** | Unity (Android, iOS, WebGL) |
| **아키텍처 패턴** | MVC, Singleton, Strategy, Facade, Service Locator, Pub/Sub Event, Object Pool |
| **외부 연동** | Firebase Auth, OpenAI API, DeepL API, Google Play Games, Apple Sign-In, ONE Store SDK |
| **결제** | Google Play Billing, Apple StoreKit, ONE Store IAP |
| **기타** | DOTween, LuaInterface, PlayMaker, XAsset, WebGL 한글 입력 지원 |
