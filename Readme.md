**🎰 Slot Machine Game**

A fully functional slot machine game built with Unity, featuring smooth
animations, realistic spinning mechanics, and engaging sound design.

**Play it now:** [[Slot Casino on
itch.io]{.underline}](https://ghanshyamkalola.itch.io/slotcasino)

**📋 Game Overview**

This is a classic slot machine game where players bet currency, pull a
lever to spin the reels, and win payouts when three identical symbols
match in the middle row. The game features realistic animations,
responsive controls, and a complete audio system.

**Core Features**

-   ✅ **Winning Logic** - Player wins when all three reels show the
    same symbol in the middle position

-   ✅ **Smooth Reel Animations** - Each reel spins continuously with
    staggered stop delays for dramatic effect

-   ✅ **Lever Animation** - Realistic lever pull with squashing
    animation and image swapping

-   ✅ **Clean Symbol Display** - Clear 3-symbol view per reel (top,
    middle, bottom)

-   ✅ **Randomized Outcomes** - Fair RNG using Unity\'s Random number
    generator

-   ✅ **Winning Payouts** - Symbol-specific payout multipliers with
    wallet management

-   ✅ **Sound Design** - Background music + 4 sound effects (lever
    pull, spin, win, lose)

-   ✅ **UI System** - Real-time wallet display, bet adjustment, win
    popup animations

-   ✅ **Debug Logging** - Comprehensive console logs for win/lose
    checking and symbol tracking

**🎮 How to Play**

**Running the WebGL Build**

1.  Visit: [[Slot Casino on
    itch.io]{.underline}](https://ghanshyamkalola.itch.io/slotcasino)

2.  Click \"Play in browser\"

3.  The game loads in your browser with no installation needed

**Game Instructions**

1.  **Adjust Your Bet**

    -   Use the + and - buttons to increase/decrease your bet amount

    -   Bet range: \$10 - \$50

    -   Your bet cannot exceed your wallet balance

2.  **Pull the Lever**

    -   Click the \"SPIN\" button to pull the lever and start spinning

    -   All reels spin simultaneously

    -   Each reel stops at different times (dramatic stagger effect)

3.  **Win Condition**

    -   Match all three symbols in the **middle row** to win

    -   Winning payout = Bet Amount × Symbol Multiplier

    -   Loss: Symbols shake animation and wallet decreases by bet

4.  **Game Over**

    -   Game ends when your wallet reaches \$0

    -   Insufficient funds warning displays when you can\'t afford the
        current bet

**🛠️ Technical Architecture**

**Project Structure**

Assets/

├── Art/

│

├── Audio/

│ ├── SFX/

│ │ ├── lever_pull.wav

│ │ ├── spinning.wav

│ │ ├── win.wav

│ │ └── lose.wav

│ └── Music/

│ └── background_music.wav

│

├── Resources/

│

├── Scenes/

│ └── MainGame.unity

│

├── Scripts/

│ ├── Core/

│ │ ├── GameEvents.cs

│ │ └── GameManager.cs

│ │

│ ├── Controllers/

│ │ ├── SpinManager.cs

│ │ ├── ReelController.cs

│ │ └── LeverController.cs

│ │

│ ├── UI/

│ │ └── UIManager.cs

│ │

│ ├── Audio/

│ │ └── AudioManager.cs

│ │

│ ├── Wallet/

│ │ └── WalletManager.cs

│ │

│ └── Data/

│ ├── SlotConfig.cs

│ └── SlotSymbol.cs

│

├── Settings/

│ └── \[Project Settings\]

│

└── Plugins/

└── \[Third-party packages\]

**Key Components**

**1. GameEvents.cs - Event System**

-   Implements Observer pattern for decoupled communication

-   Events: BetChanged, SpinStarted, ReelStopped, AllReelsStopped, Win,
    Lose, etc.

-   Allows systems to communicate without direct references

**2. SpinManager.cs - Spin Orchestration**

-   Manages complete spin sequence (lever pull → spin → result)

-   Implements win condition checking

-   Controls reel stop timing

-   Debug logging for transparency

// Win check verifies all three middle symbols match

if (symbol1 == symbol2 && symbol2 == symbol3)

// Player wins!

**3. ReelController.cs - Reel Animation**

-   Handles individual reel spinning animation

-   Properly tracks middle symbol for win condition

-   Implements shake animation on loss

-   Uses DOTween for smooth animation

**4. LeverController.cs - Lever Animation**

-   Two GameObject system (standing + pulled position)

-   Squash animation from top as lever pulls down

-   Smooth image swapping during animation

-   Elastic spring-back on reset using Ease.OutElastic

**Key Features:**

-   Fast pull down (0.3s) with 40% squash

-   Brief bounce effect at bottom

-   Hold position (0.4s)

-   Spring back with overshoot (satisfying feel)

**5. UIManager.cs - User Interface**

-   Real-time wallet display

-   Bet adjustment controls

-   Win popup with scale animation

-   Insufficient funds warning

-   Spin button state management

**6. WalletManager.cs - Financial System**

-   Manages player wallet and bet amounts

-   Handles bet placement and winnings

-   Prevents betting more than wallet balance

-   Tracks total wins separately

**7. AudioManager.cs - Sound System**

-   Singleton pattern for global audio control

-   Two AudioSources: SFX and BGM

-   Background music loops continuously

-   Sound effects triggered on game events

-   Volume control for both SFX and BGM

-   Mute functionality

**🐛 Debug Features**

The game includes comprehensive console logging for development:

\[SpinManager\] === SPIN SEQUENCE STARTED ===

\[SpinManager\] Bet placed: \$10

\[Reel 0\] Stopped - Middle symbol set to: Apple

\[Reel 1\] Stopped - Middle symbol set to: Bell

\[Reel 2\] Stopped - Middle symbol set to: Apple

\[SpinManager\] === WIN CHECK ===

\[SpinManager\] Reel 1 middle: Apple

\[SpinManager\] Reel 2 middle: Bell

\[SpinManager\] Reel 3 middle: Apple

\[SpinManager\] ✗ LOSE! Symbols don\'t match

Enable/disable debug logs by searching for Debug.Log in code.

**🎯 Configuration (SlotConfig)**

Customize game behavior through the SlotConfig ScriptableObject:

\[Header(\"Game Settings\")\]

public int startingWallet = 100; // Initial money

public int minBet = 10; // Minimum bet

public int maxBet = 50; // Maximum bet

public int betIncrement = 10; // Bet step

\[Header(\"Spin Settings\")\]

public float spinSpeed = 0.1f; // Symbol update frequency

public float reel1StopDelay = 1.5f; // When reel 1 stops

public float reel2StopDelay = 2.5f; // When reel 2 stops

public float reel3StopDelay = 3.5f; // When reel 3 stops

\[Header(\"Animation Settings\")\]

public float leverPullDuration = 0.5f; // Total lever animation time

public float winPopupDuration = 2f; // Win popup display duration

public float shakeDuration = 0.5f; // Shake animation length

public float shakeStrength = 10f; // Shake intensity

**📝 Code Quality**

**Best Practices Implemented**

✅ **SOLID Principles**

-   Single Responsibility: Each script handles one concern

-   Open/Closed: Systems can be extended without modification

-   Dependency Inversion: Event-based communication instead of direct
    coupling

✅ **Design Patterns**

-   **Observer Pattern:** GameEvents for decoupled communication

-   **Singleton Pattern:** AudioManager and SpinManager instances

-   **Configuration Pattern:** SlotConfig ScriptableObject for easy
    tuning

✅ **Code Standards**

-   Clear naming conventions (e.g., PlayLeverPullSound(),
    CheckWinCondition())

-   XML documentation comments on critical methods

-   Consistent indentation and formatting

-   Debug logging at key points

✅ **Memory Management**

-   Proper event unsubscription in OnDestroy()

-   Tween cleanup on object destruction

-   No memory leaks or dangling references

**🚀 How to Build WebGL Locally**

1.  Open the project in Unity

2.  Go to File → Build Settings

3.  Select WebGL platform

4.  Click Build and select output folder

5.  Upload build files to itch.io or any web host

**🎨 Bonus Features**

-   **Elastic Lever Reset:** Uses Ease.OutElastic for satisfying
    spring-back feel

-   **Staggered Reel Stops:** Each reel stops at different times for
    dramatic effect

-   **Sound System:** Full audio with background music and contextual
    SFX

-   **Debug Transparency:** Console logs show exact middle symbols being
    checked

-   **Responsive UI:** Real-time updates for wallet, bet, and game state

**📊 Testing Checklist**

-   ✅ Win condition correctly identifies matching symbols

-   ✅ Wallet updates properly on bet and win

-   ✅ Lever animation smooth and responsive

-   ✅ Reel animations spin and stop correctly

-   ✅ Win/lose sounds play on correct events

-   ✅ UI updates in real-time

-   ✅ Game handles insufficient funds properly

-   ✅ Console logs show correct middle symbols

-   ✅ Multiple spins work correctly

-   ✅ All animations complete without errors

**📬 Questions & Support**

For issues or questions about the game:

1.  Check console logs (F12 → Console in browser)

2.  Review SlotConfig settings

3.  Verify audio files are assigned in AudioManager

**📄 License**

This project is provided as-is for educational and commercial purposes.

**Enjoy the game! 🎉**
