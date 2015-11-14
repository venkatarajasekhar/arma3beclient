﻿using Arma3BE.Server.Decorators;
using Arma3BEClient.Common.Logging;
using BattleNET;

namespace Arma3BE.Server
{
    public interface IBattlEyeClientFactory
    {
        IBattlEyeClient Create(BattlEyeLoginCredentials credentials);
    }

    public class BattlEyeClientFactory : IBattlEyeClientFactory
    {
        private readonly ILog _log;

        public BattlEyeClientFactory(ILog log)
        {
            _log = log;
        }

        public IBattlEyeClient Create(BattlEyeLoginCredentials credentials)
        {
            return new ThreadSafeBattleEyeClient(new BattlEyeClientProxy(new BattlEyeClient(credentials)), _log);
        }
    }

    public class WatcherBEClientFactory : IBattlEyeClientFactory
    {
        private readonly ILog _log;

        public WatcherBEClientFactory(ILog log)
        {
            _log = log;
        }

        public IBattlEyeClient Create(BattlEyeLoginCredentials credentials)
        {
            return new BEConnectedWatcher(new BattlEyeClientFactory(_log), _log, credentials);
        }
    }
}