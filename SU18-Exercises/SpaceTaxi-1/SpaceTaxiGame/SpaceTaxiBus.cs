using DIKUArcade.EventBus;

namespace SpaceTaxi_1.SpaceTaxiGame {
    public static class SpaceBus {
        private static GameEventBus<object> eventBus;
        
        /// <summary>
        /// Instantiates or returns the SpaceBus singleton
        /// </summary>
        /// <returns>Returns an instance of SpaceBus</returns>
        public static GameEventBus<object> GetBus() {
            return SpaceBus.eventBus ?? (SpaceBus.eventBus =
                       new GameEventBus<object>());
        }
    }
}