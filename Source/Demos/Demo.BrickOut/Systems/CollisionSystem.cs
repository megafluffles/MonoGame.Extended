using System.Collections.Generic;
using Demo.BrickOut.Components;
using Demo.BrickOut.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Collections;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Components;

namespace Demo.BrickOut.Systems
{
    [EntitySystem(GameLoopType.Update, Layer = 0)]
    public class CollisionSystem : EntitySystem
    {
        protected override void Process(GameTime gameTime)
        {
            var paddles = EntityManager.GetEntitiesByGroup(EntityGroup.Paddles);
            var balls = EntityManager.GetEntitiesByGroup(EntityGroup.Balls);
            var bricks = EntityManager.GetEntitiesByGroup(EntityGroup.Bricks);

            foreach (var paddle in paddles)
            {
                CheckPaddleAgainstWalls(paddle);
                CheckPaddleAgainstBalls(paddle, balls);
            }

            CheckBallsAgainstBricks(balls, bricks);
        }

        private static void CheckBallsAgainstBricks(Bag<Entity> balls, Bag<Entity> bricks)
        {
            foreach (var ball in balls)
            {
                var ballRectangle = GetBoundingRectangle(ball);

                foreach (var brick in bricks)
                {
                    var brickRectangle = GetBoundingRectangle(brick);

                    if (ballRectangle.Intersects(brickRectangle))
                    {
                        var ballBody = ball.Get<BodyComponent>();
                        ballBody.Velocity.Y = -ballBody.Velocity.Y;
                        brick.Destroy();
                    }
                }
            }
        }

        private static void CheckPaddleAgainstBalls(Entity paddle, IEnumerable<Entity> balls)
        {
            var paddleRectangle = GetBoundingRectangle(paddle);

            foreach (var ball in balls)
            {
                var ballRectangle = GetBoundingRectangle(ball);

                if (paddleRectangle.Intersects(ballRectangle))
                {
                    var ballBody = ball.Get<BodyComponent>();
                    ballBody.Velocity.Y = -ballBody.Velocity.Y;
                }
            }
        }

        private static void CheckPaddleAgainstWalls(Entity paddle)
        {
            var transform = paddle.Get<TransformComponent>();
            var body = paddle.Get<BodyComponent>();
            var mouseState = Mouse.GetState();

            transform.Position = new Vector2(mouseState.Position.X, transform.Position.Y);

            if (transform.Position.X - body.Size.Width / 2f <= 0)
                transform.Position = new Vector2(body.Size.Width / 2f, transform.Position.Y);

            if (transform.Position.X + body.Size.Width / 2f >= GameMain.VirtualWidth)
                transform.Position = new Vector2(GameMain.VirtualWidth - body.Size.Width / 2f, transform.Position.Y);
        }

        private static RectangleF GetBoundingRectangle(Entity entity)
        {
            var transform = entity.Get<TransformComponent>();
            var body = entity.Get<BodyComponent>();
            return new RectangleF(transform.Position.X - body.Size.Width / 2f, transform.Position.Y - body.Size.Height / 2f, body.Size.Width, body.Size.Height);
        }
    }
}