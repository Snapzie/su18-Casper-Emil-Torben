using DIKUArcade.Entities;
using DIKUArcade.EventBus;
using DIKUArcade.Physics;
using DIKUArcade.Timers;
using SpaceTaxi_1.Customers;
using SpaceTaxi_1.Entities;
using SpaceTaxi_1.SpaceTaxiGame;

namespace SpaceTaxi_1.SpaceTaxiStates {
    public partial class GameRunning {
        /// <summary>
        /// Does collision detection by iterating all blocks, platforms and customers
        /// </summary>
        public void IterateCollisions() {
            bool collisionDetected = false;
            foreach (Platform platform in levelContainer[0]) {
                if (CollisionDetection.Aabb((DynamicShape) player.Entity.Shape, platform.Shape)
                    .Collision) {
                    collisionDetected = true;
                    //Collision with platform from bellow
                    if (((DynamicShape) (player.Entity.Shape)).Direction.Y > 0) {
                        BelowPlatform();
                    } //Collision with platform too fast
                    else if (((DynamicShape) (player.Entity.Shape)).Direction.Y < -0.004f) {
                        CrashingPlatform();
                    } //Landed on platform 
                    else {
                        LandingPlatform(platform);
                    }
                }
            }


            foreach (Entity block in levelContainer[1]) {
                if (CollisionDetection.Aabb((DynamicShape) player.Entity.Shape, block.Shape)
                    .Collision) {
                    collisionDetected = true;
                    SpaceBus.GetBus().RegisterEvent(
                        GameEventFactory<object>.CreateGameEventForAllProcessors(
                            GameEventType.GameStateEvent, this, "CHANGE_STATE", "GameLost", ""));
                }
            }

            foreach (Customer customer in levelContainer[2]) {
                if (CollisionDetection.Aabb((DynamicShape) player.Entity.Shape, customer.Shape)
                    .Collision) {
                    if (currentCustomer == null) {
                        collisionDetected = true;
                        currentCustomer = customer;
                        customer.PickUpTime = StaticTimer.GetElapsedSeconds();
                        RemoveCustomer(customer);
                    }
                }
            }

            if (!collisionDetected) {
                if (player.Entity.Shape.Position.Y > 1) {
                    SpaceBus.GetBus().RegisterEvent(
                        GameEventFactory<object>.CreateGameEventForAllProcessors(
                            GameEventType.GameStateEvent, this, "CHANGE_STATE", "GameRunning",
                            (levelNumber + 1).ToString()));
                    if (currentCustomer != null) {
                        currentCustomer.CrossedBorder = true;
                    }
                }
            }
        }

        /// <summary>
        /// Handles the what happens when the player hits a block from below
        /// </summary>
        private void BelowPlatform() {
            SpaceBus.GetBus().RegisterEvent(
                GameEventFactory<object>.CreateGameEventForAllProcessors(
                    GameEventType.GameStateEvent, this, "CHANGE_STATE", "GameLost", ""));
        }

        /// <summary>
        /// Handles the what happens when the player hits a block too fast
        /// </summary>
        private void CrashingPlatform() {
            SpaceBus.GetBus().RegisterEvent(
                GameEventFactory<object>.CreateGameEventForAllProcessors(
                    GameEventType.GameStateEvent, this, "CHANGE_STATE", "GameLost", ""));
        }

        /// <summary>
        /// Handles the what happens when the player will land on a platform
        /// </summary>
        /// <param name="platform">The platform to collide with</param>
        private void LandingPlatform(Platform platform) {
            player.SetDirrection(0, 0);
            player.SetForce(0, 0);
            player.SetGravity(false);
            if (currentCustomer != null &&
                currentCustomer.DestinationPlatform.Contains("^") ==
                currentCustomer.CrossedBorder) {
                //We are in correct level
                if (currentCustomer.DestinationPlatform.Length == 1) {
                    if (currentCustomer.DestinationPlatform[0] == '^') {
                        points += currentCustomer.CalculatePoints();
                        currentCustomer = null;
                    } else if (currentCustomer.DestinationPlatform[0] == platform.Identifier) {
                        points += currentCustomer.CalculatePoints();
                        currentCustomer = null;
                    }
                } else if (currentCustomer.DestinationPlatform[1] ==
                           platform.Identifier) {
                    points += currentCustomer.CalculatePoints();
                    currentCustomer = null;
                }
            }
        }

        private EntityContainer<Entity>[] levelContainer;
    }
}