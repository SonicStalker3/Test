using System;
using System.Collections.Generic;

public static class NPCManager
{
    private static readonly Dictionary<string, NPC> npcDictionary = new Dictionary<string, NPC>();

    public static void RegisterNPC(NPC npc)
    {
        if (npcDictionary.ContainsKey(npc.name))
        {
            throw new Exception($"NPC with name '{npc.name}' already exists.");
        }

        npcDictionary[npc.name] = npc;
    }

    public static NPC GetNPC(string name)
    {
        if (npcDictionary.TryGetValue(name, out var npc))
        {
            return npc;
        }
        else
        {
            throw new Exception($"NPC with name '{name}' not found.");
        }
    }
}
