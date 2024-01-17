push 하실 때 Library 폴더를 반드시 삭제 해 주세요.
나중에 점점 용량 커져서 깃에 못올려요
.gitignore 에 적어놨는데 계속 같이 올라감.. 방법 아시는분

ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ

[ Feat: Set Default UI ]

1. 게임의 시작 지점은 DontDestory 씬 입니다.
2. 각자 개발중이신 기능은 Assets/Scene/Stage1~3 중 하나에 적용해주세요.
3. 혹시라도 컨플릭트 우려되시는 분들은 서로 다른 스테이지에 적용하시면 됩니다.
4. UI 는 기획자의 의도에 따라 언제든지 변경될 수 있습니다.

ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ

[ Feat: Add Resoulution Selection ]

1. 사운드 / 해상도 패널을 추가하였습니다.
2. 모든 리소스는 Resources 하위 폴더로 이동하였습니다.

ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ

[ Feat : Add Stage Lock & Save & Load ]

# Scenes/Stages 의 씬 이름 & Images/Stages 의 파일 이름은 수정하지 말아주세요.

1. 스테이지 Lock / UnLock / Clear 분류가 추가되었습니다.
2. 저장 / 불러오기를 추가하였습니다. (PlayerPrefs, 현재는 스테이지 클리어 기록만 저장)

ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ

[ Feat : Add SFXmixer & Sprites folder ]

배경음(메인 화면, 스테이지 배경, 마지막 스테이지 파도, 소음)은 BGMmixer로
효과음은 SFXmixer로 따로 조절할 것 같습니다

SoundManager에 음소거 버튼, 볼륨 조절 슬라이드에 대응하는 함수를 넣었습니다

Resources에서 로드할 필요가 없는 이미지, 스프라이트들은 Sprites 폴더에 넣으면 됩니다

ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ

[ Fix: Repair Menu On/Off after Clear ]

1. 메뉴 창이 켜져있는 상태로 게임 클리어 시, 메뉴 창이 닫히지 않는 버그를 수정하였습니다.

ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ

[ Feat: Add Menu & Settings ]

1. 기획상 기존 Menu (해상도, 사운드 패널) 는 Settings 로 변경되었습니다.
2. Scripts/Scene 폴더 명을 UI 로 변경했습니다.
3. Scripts/Main & DontDestroy 스크립트도 Scripts/UI 폴더로 이동했습니다.