using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

using RiverRaid.Generics.SpritesHandle;

namespace RiverRaid.Elementes
{
    enum EnumEnemyOrientation { DOWN = 0, LEFT = 1, RIGHT = 2 };

    class EnemyFactory
    {
        private ContentManager spriteLoader;

        private Random random;
        private int timeForNextEnemy;
        private int timeCountForNextEnemy;
        private int factorNextEnemyTime;

        private Rectangle enemyBounds;

        private Single refPlayerSpeed;

        private static EnemyFactory singleton;
        public static EnemyFactory Singleton
        {
            get { return EnemyFactory.singleton; }
        }

        private List<Enemy> listEnemy;
        public List<Enemy> ListEnemy
        {
            get { return listEnemy; }
        }

        private EnemyFactory(ContentManager spriteLoader, Rectangle enemyBounds, ref Single playerSpeed)
        {
            this.spriteLoader = spriteLoader;
            this.listEnemy = new List<Enemy>();
            this.random = new Random();

            this.enemyBounds = enemyBounds;
            this.refPlayerSpeed = playerSpeed;
            this.timeCountForNextEnemy = 0;
            this.factorNextEnemyTime = 200;
            this.timeForNextEnemy = factorNextEnemyTime * random.Next(20);
        }

        public static void Initialize(ContentManager spriteLoader, Rectangle enemyBounds, ref Single playerSpeed)
        {
            singleton = new EnemyFactory(spriteLoader, enemyBounds, ref playerSpeed);
        }


        public void Uptade(GameTime gameTime)
        {
            timeCountForNextEnemy += gameTime.ElapsedGameTime.Milliseconds;
            if (timeCountForNextEnemy >= timeForNextEnemy)
            {
                EnumEnemyOrientation orientation = (EnumEnemyOrientation)random.Next(3);
                Texture2D enemyTexture;
                Vector2 position;
                Vector2 direction;

                switch (orientation)
                {
                    case EnumEnemyOrientation.DOWN:
                        enemyTexture = spriteLoader.Load<Texture2D>(@"Images\Enemies\A7E\A7E-Down");
                        position = new Vector2(random.Next(800), 0);
                        direction = new Vector2(0, 2 * (random.Next(5) + 1));
                        listEnemy.Add(new Enemy(enemyTexture, position, direction, enemyBounds, 
                            new Point(5, 45), ref refPlayerSpeed));
                        break;
                    case EnumEnemyOrientation.RIGHT:
                        enemyTexture = spriteLoader.Load<Texture2D>(@"Images\Enemies\A7E\A7E-Right");
                        position = new Vector2(0, random.Next(300));
                        direction = new Vector2(2 *(random.Next(5) + 1), 1);
                        listEnemy.Add(new Enemy(enemyTexture, position, direction, enemyBounds,
                            new Point(35, 5), ref refPlayerSpeed));
                        break;
                    case EnumEnemyOrientation.LEFT:
                        enemyTexture = spriteLoader.Load<Texture2D>(@"Images\Enemies\A7E\A7E-Left");
                        position = new Vector2(800, random.Next(300));
                        direction = new Vector2(-4 * (random.Next(5) + 1), 1);
                        listEnemy.Add(new Enemy(enemyTexture, position, direction, enemyBounds, 
                            new Point(35, 5), ref refPlayerSpeed));
                        break;
                }
                timeCountForNextEnemy = 0;
                this.timeForNextEnemy = factorNextEnemyTime * random.Next(20);
            }
        }

        public void ResetFactory()
        {
            listEnemy.Clear();
            this.timeCountForNextEnemy = 0;
            this.factorNextEnemyTime = 200;
            this.timeForNextEnemy = factorNextEnemyTime * random.Next(20);
        }
    }
}
