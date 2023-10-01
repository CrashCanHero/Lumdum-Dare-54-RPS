using Godot;
using System;

public partial class SaveSystem : Node2D {
    public static SaveSystem Instance;

    byte[] key = new byte[] {
        0x01,
        0x93,
        0x97,
        0x26,
        0x99,
        0x56,
        0x88,
        0x94,
        0x47,
        0x32,
        0x36,
        0x93,
        0x75,
        0x79,
        0x61,
        0x62,
        0x94,
    };

    public override void _Ready() {
        if (Instance != null) {
            QueueFree();
            return;
        }

        Instance = this;
    }

    public void Save(int score) {
        FileAccess file = FileAccess.OpenEncrypted("user://game.sav", FileAccess.ModeFlags.Write, key);
        file.Store64(MicroGameManager.GetFrogs());   
    }

    public ulong Load() {
        if (!FileAccess.FileExists("user://gmae.sav")) {
            return 0;
        }

        FileAccess file = FileAccess.OpenEncrypted("user://game.sav", FileAccess.ModeFlags.Read, key);

        return file.Get64();
    }
}
