# SparataDungeon

<br>

# 🎮 게임 실행 화면 프리뷰
![시연 영상](https://github.com/user-attachments/assets/71cdf7d8-31b0-4f8a-9c29-b6c92d9e1aed)


# 📂 프로젝트 구조
```Scripts
┣ 📂Player
┃ ┣ 📜CharacterManager.cs
┃ ┣ 📜InterAction.cs
┃ ┣ 📜Player.cs
┃ ┣ 📜PlayerController.cs
┃ ┣ 📜PlayerState.cs
┣ 📂Scriptable
┃ ┣ 📂Data
┃ ┃ ┣ 📂Item
┃ ┃ ┃ ┣ 📜HealthPotion.asset
┃ ┃ ┃ ┣ 📜JumpPotion.asset
┃ ┃ ┃ ┣ 📜SpeedPotion.asset
┃ ┃ ┣ 📂Object
┃ ┃ ┃ ┣ 📜Buildings.asset
┃ ┃ ┃ ┣ 📜CarRoads.asset
┃ ┃ ┃ ┣ 📜Cars.asset
┃ ┃ ┃ ┣ 📜JumpObject.asset
┃ ┃ ┃ ┣ 📜Roads.asset
┃ ┣ 📜InteractableObject.cs
┃ ┣ 📜ItemData.cs
┃ ┣ 📜Items.cs
┃ ┣ 📜JumpObject.cs
┃ ┣ 📜MapObject.cs
┃ ┣ 📜ObjectData.cs
┣ 📂UI
┃ ┣ 📜UIInventory.cs
┃ ┣ 📜UIManager.cs
┃ ┣ 📜UISlot.cs
┃ ┣ 📜UIState.cs
```

<br>

# 🎮 게임 플레이 가이드
- WASD : 이동
- Space : 점프
- Shift : 달리기
- E : 상호작용
- Q : 아이템 버리기
- 마우스 : 시야 
- 마우스 휠 : 인벤토리 선택 
- 마우스 좌클릭 : 아이템 사용 

# ⚙ 주요 시스템
### 달리기와 점프

Shift 달리기와 Space 점프를 이용해 맵의 제일 높은 곳으로 이동 
![달리기 영상](https://github.com/user-attachments/assets/a97a044e-c989-45c0-91db-a7f6bb55d494)
![점프 영상](https://github.com/user-attachments/assets/0cd00f5f-c73a-4dc2-bdfc-259dbd9beddd)


### 아이템 
체력 회복 
![체력 물약](https://github.com/user-attachments/assets/f8ed8f7d-bb94-4595-b67d-2e04b5e51aa0)

이동 속도 증가 
![속도 물약](https://github.com/user-attachments/assets/6b589c0e-891e-4c99-8cc9-3e9a899a3a3f)

점프력 증가 
![점프 물약](https://github.com/user-attachments/assets/782be890-0a6c-4e5a-b1e0-d4afd888b864)


### 맵 오브젝트 
점프력이 부족하다면 점프대 이용 가능 
![점프대](https://github.com/user-attachments/assets/6b5d6ed7-3b4a-4a9d-a23a-58d2b378d407)



