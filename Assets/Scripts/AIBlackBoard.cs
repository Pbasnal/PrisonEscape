using System;
using System.Collections.Generic;

public static class AIBlackBoard
{
    private static RoomContainer roomContainer;
    private static RoomBuilder[,] _levelLayout;

    static AIBlackBoard()
    {
        roomContainer = new RoomContainer();
    }

    public static void AddLevelLayout(RoomBuilder[,] levelLayout)
    {
        _levelLayout = levelLayout;
    }

    public static void AddEnemyAndRoom(Guid roomId, BasicSeekerAI seekerAI)
    {
        roomContainer._container.Add(roomId, new EnemyContainer());
    }
}

public class RoomContainer
{
    public IDictionary<Guid, EnemyContainer> _container;

    public RoomContainer()
    {
        _container = new Dictionary<Guid, EnemyContainer>();
    }

    public void Add(Guid roomId, BasicSeekerAI enemy)
    {
        if (!_container.ContainsKey(roomId))
        {
            _container.Add(roomId, new EnemyContainer());
        }
        _container[roomId].Add(enemy);
    }
}

public class EnemyContainer
{
    public IDictionary<Guid, BasicSeekerAI> _container;

    public EnemyContainer()
    {
        _container = new Dictionary<Guid, BasicSeekerAI>();
    }

    public void Add(BasicSeekerAI enemy)
    {
        if (!_container.ContainsKey(enemy.enemyId))
        {
            _container.Add(enemy.enemyId, enemy);
        }
    }
}