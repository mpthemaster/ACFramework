
using System;
using System.Drawing;
using System.Windows.Forms;

namespace ACFramework
{ 
	
	class cCritterDoor : cCritterWall 
	{

	    public cCritterDoor(cVector3 enda, cVector3 endb, float thickness, float height, cGame pownergame ) 
		    : base( enda, endb, thickness, height, pownergame ) 
	    { 
	    }
		
		public override bool collide( cCritter pcritter ) 
		{ 
			bool collided = base.collide( pcritter ); 
			if ( collided && pcritter.IsKindOf( "cCritter3DPlayerHomer" ) ) 
			{ 
				(( cGame3D ) Game ).setdoorcollision( ); 
				return true; 
			} 
			return false; 
		}
 
        public override bool IsKindOf( string str )
        {
            return str == "cCritterDoor" || base.IsKindOf( str );
        }

        public override string RuntimeClass
        {
            get
            {
                return "cCritterDoor";
            }
        }
	} 
	
	//==============Critters for the cGame3D: Player, Ball, Treasure ================ 
    // Doesn't work yet, so I commented it out. I need a force for it first.
    
    class cCritterWallMoving : cCritterWall
    {
        private float speed = 2.0f;
        private float timeToTurn = 1.0f;

        
        public cCritterWallMoving(cVector3 enda, cVector3 endb, float thickness, float height, cGame pownergame)
            : base(enda, endb, thickness, height, pownergame)
        {
        }

        public override void update(ACView pactiveview, float dt)
        {
            base.update(pactiveview, dt);
            
            dragTo(Position.add(new cVector3(speed * dt,0,0)),dt);

            timeToTurn -=dt;
            if (timeToTurn<=0)
            {
                speed *= -1;
                timeToTurn = 1.0f;
            }
        }

        public override bool collide(cCritter pcritter)
        {
            bool collided = base.collide(pcritter);
            if (collided && pcritter.IsKindOf("cCritter3DPlayerHomer"))
            {
                cCritter3DPlayerHomer a = (cCritter3DPlayerHomer)pcritter;
                a.dragTo(a.Position.add(new cVector3(speed * Framework.pdoc.getdt(), 0, 0)), Framework.pdoc.getdt());
            }
            return false;
        }

        public override bool IsKindOf(string str)
        {
            return str == "cCritterWallMoving" || base.IsKindOf(str);
        }

        public override string RuntimeClass
        {
            get
            {
                return "cCritterWallMoving";
            }
        }
    }
    class cCritterHealth : cCritterWall
    {
        public cCritterHealth(cVector3 enda, cVector3 endb, float thickness, float height, cGame pownergame)
            : base(enda, endb, thickness, height, pownergame)
        {
        }

        public override bool collide(cCritter pcritter)
        {
            bool collided = base.collide(pcritter);
            if (collided && pcritter.IsKindOf("cCritter3DPlayerHomer"))
            {
                //I can't use (cCritter3DPlayerHomer)pcritter.keys += 1; 
                //so I had to do it in a backwards manner to get it to work.
                cCritter3DPlayerHomer a = (cCritter3DPlayerHomer)pcritter;
                a.addHealth(20);
                die();
                return true;
            }
            return false;
        }


        public override bool IsKindOf(string str)
        {
            return str == "cCritterHealth" || base.IsKindOf(str);
        }

        public override string RuntimeClass
        {
            get
            {
                return "cCritterHealth";
            }
        }
    } 

    class cCritterLava : cCritterWall
    {
        public cCritterLava(cVector3 enda, cVector3 endb, float thickness, float height, cGame pownergame)
            : base(enda, endb, thickness, height, pownergame)
        {
        }

        public override bool collide(cCritter pcritter)
        {
            bool collided = base.collide(pcritter);
            if (collided && pcritter.IsKindOf("cCritter3DPlayerHomer"))
            {
                cCritter3DPlayerHomer a = (cCritter3DPlayerHomer)pcritter;
                if (a.cheater == false) //To prevent it from killing the player while in cheat mode.
                {
                    a.damage(200);
                }
                return true;
            }
            return false;
        }


        public override bool IsKindOf(string str)
        {
            return str == "cCritterLava" || base.IsKindOf(str);
        }

        public override string RuntimeClass
        {
            get
            {
                return "cCritterLava";
            }
        }
    } 

    class cCritterDoorLocked : cCritterWall
    {

        public cCritterDoorLocked(cVector3 enda, cVector3 endb, float thickness, float height, cGame pownergame)
            : base(enda, endb, thickness, height, pownergame)
        {
        }

        public override bool collide(cCritter pcritter)
        {
            bool collided = base.collide(pcritter);
            if (collided && pcritter.IsKindOf("cCritter3DPlayerHomer"))
            {
                //I can't use (cCritter3DPlayerHomer)pcritter.keys += 1; 
                //so I had to do it in a backwards manner to get it to work.
                cCritter3DPlayerHomer a = (cCritter3DPlayerHomer)pcritter;
                if (a.keys > 0)
                {
                    a.keys -= 1;
                    this.die();
                }
                return true;
            }
            return false;
        }

        public override bool IsKindOf(string str)
        {
            return str == "cCritterDoorLocked" || base.IsKindOf(str);
        }

        public override string RuntimeClass
        {
            get
            {
                return "cCritterDoorLocked";
            }
        }
    } 
    
    class cCritterKey : cCritterWall
    {

        public cCritterKey(cVector3 enda, cVector3 endb, float thickness, float height, cGame pownergame)
            : base(enda, endb, thickness, height, pownergame)
        {
        }

        public override bool collide(cCritter pcritter)
        {

            bool collided = base.collide(pcritter);
            if (collided && pcritter.IsKindOf("cCritter3DPlayerHomer"))
            {
                //I can't use (cCritter3DPlayerHomer)pcritter.keys += 1; 
                //so I had to do it in a backwards manner to get it to work.
                cCritter3DPlayerHomer a = (cCritter3DPlayerHomer)pcritter;
                a.keys += 1;
                
                this.die();
                return true;
            }
            return false;
        }
        
        public override bool IsKindOf(string str)
        {
            return str == "cCritterKey" || base.IsKindOf(str);
        }

        public override string RuntimeClass
        {
            get
            {
                return "cCritterKey";
            }
        }
    } 

    /*
     * Bug: changing the player's max speed because of the poison also slows down the player's falling speed, 
     * and if he is already high up before falling at a slow speed, he can repeatedly jump in the air.
     */
    class cCritter3DPlayerHomer : cCritterArmedPlayer
    {
        private int poisonAmount = 0;
        public int keys = 0;

        public bool cheater = false;

        private float recoverTime = 4;
        private float currentRecoverTime = 0;

        //because MaxSpeed is changed around with poison, this is used to remember the player's unpoisoned speed.
        private float normalMaxSpeed;

        private int killCount = 0; //how many kills the player has gotten in room 3

        public cCritter3DPlayerHomer(cGame pownergame)
            :base( pownergame)
        {
            BulletClass = new cCritterPlutonium();
            Sprite = new cSpriteQuake(ModelsMD2.Homer);
            Sprite.SpriteAttitude = cMatrix3.scale(2, 0.8f, 0.4f);
            setRadius(0.5f); //Default cCritter.PLAYERRADIUS is 0.4.  
            setHealth(100);
            moveTo(_movebox.LoCorner.add(new cVector3(0.0f, 0.0f, 2.0f)));
            WrapFlag = cCritter.CLAMP; //Use CLAMP so you stop dead at edges.
            Armed = true; //Let's use bullets.
            MaxSpeed = cGame3D.MAXPLAYERSPEED;
            normalMaxSpeed = MaxSpeed;
            AbsorberFlag = true; //Keeps player from being buffeted about.
            ListenerAcceleration = 160.0f; //So Hopper can overcome gravity.  Only affects hop.

            Listener = new cListenerPlayer(0.2f, 12.0f); 
            // the two arguments are walkspeed and hop strength -- JC

            addForce(new cForceGravity(50.0f)); /* Uses  gravity. Default strength is 25.0.
			Gravity	will affect player using cListenerHopper. */
            AttitudeToMotionLock = false; //It looks nicer is you don't turn the player with motion.
            Attitude = new cMatrix3(new cVector3(0.0f, 0.0f, -1.0f), new cVector3(-1.0f, 0.0f, 0.0f),
                new cVector3(0.0f, 1.0f, 0.0f), Position);

            Sprite.ModelState = State.Idle;
        }

        public override void update(ACView pactiveview, float dt)
        {
            base.update(pactiveview, dt); //Always call this first

            //Every three poison will cut speed in half. All the casting made it ugly, here's the math without all the excess: 1/(2^((poisonAmount)/3))
            MaxSpeed = normalMaxSpeed * (float)(1.0 / (Math.Pow(2.0, (poisonAmount / 3.0))));

            if (poisonAmount > 0)
            {
                currentRecoverTime -= dt;
                if (currentRecoverTime <= 0)
                {
                    poisonAmount -= 1;
                    currentRecoverTime = recoverTime;
                }
                if (poisonAmount>9)
                { 
                    poisonAmount = 9;
                }
            }
            if (cheater)
            {
                Health = 100;
            }
        }

        
        public int getKillCount()
        {
            return killCount;
        }
        public void increaseKillCount()
        {
            killCount += 1;
        }
        public void resetKillCount()
        {
            killCount = 0;
        }

        public override bool collide(cCritter pcritter)
        {
            bool collided = base.collide(pcritter);

            //Check if the critter is dead, if so the collision doesn't matter.
            if (pcritter is cCritterBigHead)
            {
                if ((pcritter as cCritterBigHead).Dead == true)
                    return false;
            }
            else if (pcritter is cCritterChicken)
            {
                if ((pcritter as cCritterChicken).Dead == true)
                    return false;
            }
            else if (pcritter is cCritterMiniBot)
            {
                if ((pcritter as cCritterMiniBot).Dead == true)
                    return false;
            }
            else if (pcritter is cCritterSailorVenus)
            {
                if ((pcritter as cCritterSailorVenus).Dead == true)
                    return false;
            }
            else if (pcritter is cCritterSnake)
            {
                if ((pcritter as cCritterSnake).Dead == true)
                    return false;
            }

            if (collided && pcritter.IsKindOf("cCritterSnake"))
            {
                poisonAmount += 3;
            }
            else  if (collided && pcritter.IsKindOf("cCritterBulletPoison"))
            {
                poisonAmount += 3;
                Framework.snd.play(Sound.poisonSplat);
            }
            else if (collided && pcritter.IsKindOf("cCritterBulletEggs"))
            {
                Framework.snd.play(Sound.eggSplat);
            }

            //The wall's collide happens when the player touches it, but the player's collide doesn't happen.
            //so picking up a key, picking up health, and opening a door has to be done in thier collide function
            
            //Below is the collide code from cCritter3DPlayer. Might need some cleaning.
            bool playerhigherthancritter = Position.Y - Radius > pcritter.Position.Y;
            /* If you are "higher" than the pcritter, as in jumping on it, you get a point
        and the critter dies.  If you are lower than it, you lose health and the
        critter also dies. To be higher, let's say your low point has to higher
        than the critter's center. We compute playerhigherthancritter before the collide,
        as collide can change the positions. */
            _baseAccessControl = 1;
            
            _baseAccessControl = 0;
            if (!collided)
                return false;




            /* If you're here, you collided.  We'll treat all the guys the same -- the collision
         with a Treasure is different, but we let the Treasure contol that collision. */
            if (playerhigherthancritter)
            {
            }
            else
            {
                damage(1);

                if (Health == 0)
                {
                    Sprite.ModelState = State.KneelDying;
                }

            }
            return true;
        }

        public override cCritterBullet shoot()
        {
            Framework.snd.play(Sound.plutonium);
            return base.shoot();
        }

        public override bool IsKindOf(string str)
        {
            return str == "cCritter3DPlayerHomer" || base.IsKindOf(str);
        }

        public override string RuntimeClass
        {
            get
            {
                return "cCritter3DPlayerHomer";
            }
        }
    }


	class cCritter3DPlayer : cCritterArmedPlayer 
	{ 
        private bool warningGiven = false;
		
        public cCritter3DPlayer( cGame pownergame ) 
            : base( pownergame ) 
		{ 
			BulletClass = new cCritter3DPlayerBullet( ); 
            Sprite = new cSpriteSphere(); 
			Sprite.FillColor = Color.DarkGreen; 
			Sprite.SpriteAttitude = cMatrix3.scale( 2, 0.8f, 0.4f ); 
			setRadius( cGame3D.PLAYERRADIUS ); //Default cCritter.PLAYERRADIUS is 0.4.  
			setHealth( 10 ); 
			moveTo( _movebox.LoCorner.add( new cVector3( 0.0f, 0.0f, 2.0f ))); 
			WrapFlag = cCritter.CLAMP; //Use CLAMP so you stop dead at edges.
			Armed = true; //Let's use bullets.
			MaxSpeed =  cGame3D.MAXPLAYERSPEED; 
			AbsorberFlag = true; //Keeps player from being buffeted about.
			ListenerAcceleration = 160.0f; //So Hopper can overcome gravity.  Only affects hop.
		
			Listener = new cListenerScooterYHopper( 0.2f, 12.0f ); 
            // the two arguments are walkspeed and hop strength -- JC
            
            addForce( new cForceGravity( 50.0f )); /* Uses  gravity. Default strength is 25.0.
			Gravity	will affect player using cListenerHopper. */ 
			AttitudeToMotionLock = false; //It looks nicer is you don't turn the player with motion.
			Attitude = new cMatrix3( new cVector3(0.0f, 0.0f, -1.0f), new cVector3( -1.0f, 0.0f, 0.0f ), 
                new cVector3( 0.0f, 1.0f, 0.0f ), Position); 
		}

        public override void update(ACView pactiveview, float dt)
        {
            base.update(pactiveview, dt); //Always call this first
            if (!warningGiven && distanceTo(new cVector3(Game.Border.Lox, Game.Border.Loy,
                Game.Border.Midz)) < 3.0f)
            {
                warningGiven = true;
                //MessageBox.Show("DON'T GO THROUGH THAT DOOR!!!  DON'T EVEN THINK ABOUT IT!!!");
            }
 
        } 

        public override bool collide( cCritter pcritter ) 
		{ 
			bool playerhigherthancritter = Position.Y - Radius > pcritter.Position.Y; 
		/* If you are "higher" than the pcritter, as in jumping on it, you get a point
	and the critter dies.  If you are lower than it, you lose health and the
	critter also dies. To be higher, let's say your low point has to higher
	than the critter's center. We compute playerhigherthancritter before the collide,
	as collide can change the positions. */
            _baseAccessControl = 1;
			bool collided = base.collide( pcritter );
            _baseAccessControl = 0;
            if (!collided) 
				return false;
		/* If you're here, you collided.  We'll treat all the guys the same -- the collision
	 with a Treasure is different, but we let the Treasure contol that collision. */ 
			if ( playerhigherthancritter ) 
			{
                Framework.snd.play(Sound.Goopy); 
			} 
			else 
			{ 
				damage( 1 );
                Framework.snd.play(Sound.Crunch); 
			} 
			pcritter.die(); 
			return true; 
		}

        public override cCritterBullet shoot()
        {
            Framework.snd.play(Sound.LaserFire);
            return base.shoot();
        }

        public override bool IsKindOf( string str )
        {
            return str == "cCritter3DPlayer" || base.IsKindOf( str );
        }
		
        public override string RuntimeClass
        {
            get
            {
                return "cCritter3DPlayer";
            }
        }
	} 
	
   
	class cCritter3DPlayerBullet : cCritterBullet 
	{

        public cCritter3DPlayerBullet() { }

        public override cCritterBullet Create()
            // has to be a Create function for every type of bullet -- JC
        {
            return new cCritter3DPlayerBullet();
        }
		
		public override void initialize( cCritterArmed pshooter ) 
		{ 
			base.initialize( pshooter );
            Sprite.FillColor = Color.Crimson;
            // can use setSprite here too
            setRadius(0.1f);
		}

        public override bool collide(cCritter pcritter)
        {
            bool collide;

            //If you hit a target, damage it and die.
            if (_baseAccessControl == 1)
                collide = base.collide(pcritter);
            else if (isTarget(pcritter))
            {
                if (!touch(pcritter))
                    collide = false;
                else
                {
                    int hitscore = pcritter.damage(_hitstrength);
                    delete_me(); //Makes a service request, but you won't go away yet.

                    collide = true;
                }
            }
            else
                //Bounce off or everything else.
                collide = base.collide(pcritter); //Bounce off non-target critters 

            if (pcritter is cCritterBigHead)
            {
                if ((pcritter as cCritterBigHead).Dead == true)
                    (pcritter as cCritterBigHead).KilledByPlayer = true;
            }
            else if (pcritter is cCritterChicken)
            {
                if ((pcritter as cCritterChicken).Dead == true)
                    (pcritter as cCritterChicken).KilledByPlayer = true;
            }
            else if (pcritter is cCritterMiniBot)
            {
                if ((pcritter as cCritterMiniBot).Dead == true)
                    (pcritter as cCritterMiniBot).KilledByPlayer = true;
            }
            else if (pcritter is cCritterSailorVenus)
            {
                if ((pcritter as cCritterSailorVenus).Dead == true)
                    (pcritter as cCritterSailorVenus).KilledByPlayer = true;
            }
            else if (pcritter is cCritterSnake)
                if ((pcritter as cCritterSnake).Dead == true)
                    (pcritter as cCritterSnake).KilledByPlayer = true;

            return collide;
        }

        public override bool IsKindOf( string str )
        {
            return str == "cCritter3DPlayerBullet" || base.IsKindOf( str );
        }
		
        public override string RuntimeClass
        {
            get
            {
                return "cCritter3DPlayerBullet";
            }
        }
	} 
	
	class cCritter3Dcharacter : cCritter  
	{ 
		
        public cCritter3Dcharacter( cGame pownergame ) 
            : base( pownergame ) 
		{ 
			addForce( new cForceGravity( 25.0f, new cVector3( 0.0f, -1, 0.00f ))); 
			addForce( new cForceDrag( 20.0f ) );  // default friction strength 0.5 
			Density = 2.0f; 
			MaxSpeed = 30.0f;
            if (pownergame != null) //Just to be safe.
                Sprite = new cSpriteQuake(Framework.models.selectRandomCritter());
            
            // example of setting a specific model
            // setSprite(new cSpriteQuake(ModelsMD2.Knight));
            
            if ( Sprite.IsKindOf( "cSpriteQuake" )) //Don't let the figurines tumble.  
			{ 
				AttitudeToMotionLock = false;   
				Attitude = new cMatrix3( new cVector3( 0.0f, 0.0f, 1.0f ), 
                    new cVector3( 1.0f, 0.0f, 0.0f ), 
                    new cVector3( 0.0f, 1.0f, 0.0f ), Position); 
				/* Orient them so they are facing towards positive Z with heads towards Y. */ 
			} 
			Bounciness = 0.0f; //Not 1.0 means it loses a bit of energy with each bounce.
			setRadius( 1.0f );
            MinTwitchThresholdSpeed = 4.0f; //Means sprite doesn't switch direction unless it's moving fast 
			randomizePosition( new cRealBox3( new cVector3( _movebox.Lox, _movebox.Loy, _movebox.Loz + 4.0f), 
				new cVector3( _movebox.Hix, _movebox.Loy, _movebox.Midz - 1.0f))); 
				/* I put them ahead of the player  */ 
			randomizeVelocity( 0.0f, 30.0f, false ); 

                        
			if ( pownergame != null ) //Then we know we added this to a game so pplayer() is valid 
				addForce( new cForceObjectSeek( Player, 0.5f ));

            int begf = Framework.randomOb.random(0, 171);
            int endf = Framework.randomOb.random(0, 171);

            if (begf > endf)
            {
                int temp = begf;
                begf = endf;
                endf = temp;
            }

			Sprite.setstate( State.Other, begf, endf, StateType.Repeat );


            _wrapflag = cCritter.BOUNCE;

		} 

		
		public override void update( ACView pactiveview, float dt ) 
		{ 
			base.update( pactiveview, dt ); //Always call this first
			if ( (_outcode & cRealBox3.BOX_HIZ) != 0 ) /* use bitwise AND to check if a flag is set. */ 
				delete_me(); //tell the game to remove yourself if you fall up to the hiz.
        } 

		// do a delete_me if you hit the left end 
	
		public override void die() 
		{ 
			Player.addScore( Value );

			base.die(); 
		} 

       public override bool IsKindOf( string str )
        {
            return str == "cCritter3Dcharacter" || base.IsKindOf( str );
        }
	
        public override string RuntimeClass
        {
            get
            {
                return "cCritter3Dcharacter";
            }
        }
	} 
	
	class cCritterTreasure : cCritter 
	{   // Try jumping through this hoop
		
		public cCritterTreasure( cGame pownergame ) : 
		base( pownergame ) 
		{ 
			/* The sprites look nice from afar, but bitmap speed is really slow
		when you get close to them, so don't use this. */ 
			cPolygon ppoly = new cPolygon( 24 ); 
			ppoly.Filled = false; 
			ppoly.LineWidthWeight = 0.5f;
			Sprite = ppoly; 
			_collidepriority = cCollider.CP_PLAYER + 1; /* Let this guy call collide on the
			player, as his method is overloaded in a special way. */ 
			rotate( new cSpin( (float) Math.PI / 2.0f, new cVector3(0.0f, 0.0f, 1.0f) )); /* Trial and error shows this
			rotation works to make it face the z diretion. */ 
			setRadius( cGame3D.TREASURERADIUS ); 
			FixedFlag = true; 
			moveTo( new cVector3( _movebox.Midx, _movebox.Midy - 2.0f, 
				_movebox.Loz - 1.5f * cGame3D.TREASURERADIUS )); 
		} 

		
		public override bool collide( cCritter pcritter ) 
		{ 
			if ( contains( pcritter )) //disk of pcritter is wholly inside my disk 
			{
                Framework.snd.play(Sound.Clap); 
				pcritter.addScore( 100 ); 
				pcritter.addHealth( 1 ); 
				pcritter.moveTo( new cVector3( _movebox.Midx, _movebox.Loy + 1.0f,
                    _movebox.Hiz - 3.0f )); 
				return true; 
			} 
			else 
				return false; 
		} 

		//Checks if pcritter inside.
	
		public override int collidesWith( cCritter pothercritter ) 
		{ 
			if ( pothercritter.IsKindOf( "cCritter3DPlayer" )) 
				return cCollider.COLLIDEASCALLER; 
			else 
				return cCollider.DONTCOLLIDE; 
		} 

		/* Only collide
			with cCritter3DPlayer. */ 

       public override bool IsKindOf( string str )
        {
            return str == "cCritterTreasure" || base.IsKindOf( str );
        }
	
        public override string RuntimeClass
        {
            get
            {
                return "cCritterTreasure";
            }
        }
	} 
	
	//======================cGame3D========================== 
	
	class cGame3D : cGame 
	{ 
		public static readonly float TREASURERADIUS = 1.2f; 
		public static readonly float WALLTHICKNESS = 0.5f; 
		public static readonly float PLAYERRADIUS = 0.2f; 
		public static readonly float MAXPLAYERSPEED = 30.0f; 
		private cCritterTreasure _ptreasure; 
		private bool doorcollision;
        private bool wentThrough = false;
        private float startNewRoom;
        private int currentRoom;
        private float timeToSpawn = 0.0f; //how long until more critters spawn in room 3.
        private bool createdDoor;//turn it to true when the door in room 3 is created so I don't re-create it.
        private int createdHealth = 0;//Keeping track of how many health packs have spawned in room 3
        public int getCurrentRoom()
        {
            return currentRoom;
        }

		public cGame3D() 
		{
            
			doorcollision = false;
			_menuflags &= ~ cGame.MENU_BOUNCEWRAP; 
			_menuflags |= cGame.MENU_HOPPER; //Turn on hopper listener option.
			_spritetype = cGame.ST_MESHSKIN;


            // Even though this will be set to something else once the game starts, omitting this line causes
            // The moving walls to have some sort of invisible barrier in the center, blocking the player from
            // moving along the z axis unless he jumps or falls off.
            setBorder(64.0f, 16.0f, 64.0f); 
		

			cRealBox3 skeleton = new cRealBox3();
            skeleton.copy(_border);
			setSkyBox( skeleton );
		    /* In this world the coordinates are screwed up to match the screwed up
		    listener that I use.  I should fix the listener and the coords.
		    Meanwhile...
		    I am flying into the screen from HIZ towards LOZ, and
		    LOX below and HIX above and
		    LOY on the right and HIY on the left. */ 
			WrapFlag = cCritter.BOUNCE; 
			_seedcount = 7; 
            
			setPlayer( new cCritter3DPlayerHomer( this )); 

		    
			float height = 0.1f * _border.YSize; 
			float ycenter = -_border.YRadius + height / 2.0f; 
			float wallthickness = cGame3D.WALLTHICKNESS;
            

            setRoom1();

		} 

        public void setRoom1( )
        {
            currentRoom = 1;

            Biota.purgeCritters("cCritterWall");
            Biota.purgeCritters("cCritter3Dcharacter");

            setBorder(64.0f, 16.0f, 64.0f); // size of the world
	        cRealBox3 skeleton = new cRealBox3();
            skeleton.copy( _border );
	        setSkyBox(skeleton);

            SkyBox.setSideTexture(cRealBox3.HIX, BitmapRes.Concrete, 1);
            SkyBox.setSideTexture(cRealBox3.LOX, BitmapRes.Concrete, 1);
            SkyBox.setSideTexture(cRealBox3.LOY, BitmapRes.Wall6, 3);
            SkyBox.setSideTexture(cRealBox3.HIZ, BitmapRes.Concrete, 1);
            SkyBox.setSideTexture(cRealBox3.LOZ, BitmapRes.Concrete, 1);
            SkyBox.setSideTexture(cRealBox3.HIY, BitmapRes.Wall1, 2);

	        _seedcount = 0;
            Player.setMoveBox(new cRealBox3(64.0f, 16.0f, 64.0f));
            float zpos = 0.0f; /* Point on the z axis where we set down the wall.  0 would be center,
			halfway down the hall, but we can offset it if we like. */
            float height = 0.1f * _border.YSize;
            float ycenter = -_border.YRadius + height / 2.0f;
            float wallthickness = cGame3D.WALLTHICKNESS;


            cCritterWall pwall = new cCritterWall(
                new cVector3(_border.Midx + 2.0f, ycenter, zpos),
                new cVector3(_border.Hix, ycenter, zpos),
                height, 
                wallthickness, 
                this);
            cSpriteTextureBox pspritebox =
                new cSpriteTextureBox(pwall.Skeleton, BitmapRes.Wall3, 16); 
            pwall.Sprite = pspritebox;


            //the key in the first part of the room
            cCritterKey pkey = new cCritterKey(
                new cVector3( 24.0f, -7, 13),
                new cVector3( 24.0f, -7, 15),
                2,
                2,
                this);
            cSpriteTextureBox testingspritebox = new cSpriteTextureBox(pkey.Skeleton, BitmapRes.Key, 1);
            pkey.Sprite = testingspritebox;

            cCritterHealth phealth = new cCritterHealth(
                new cVector3( 3.0f, -7, 0),
                new cVector3( 3.0f, -7, 2.0f),
                2,
                2,
                this);
            cSpriteTextureBox healthspritebox = new cSpriteTextureBox(phealth.Skeleton, BitmapRes.Health, 1);
            phealth.Sprite = healthspritebox;

            //The wall above the first door.
            cCritterWall wall1 = new cCritterWall(
                new cVector3(0, 3, 5),
                new cVector3(0, 3, 4),
                2,
                16,
                this);
            cSpriteTextureBox spritebox1 =
                new cSpriteTextureBox(wall1.Skeleton, BitmapRes.Wall3, 1);
            wall1.Sprite = spritebox1;

            //The wall to the left of the first door
            cCritterWall wall2 = new cCritterWall(
                new cVector3(-16.5f, 0, 5),
                new cVector3(-16.5f, 0, 4),
                31f,
                16,
                this);
            cSpriteTextureBox spritebox2 =
                new cSpriteTextureBox(wall2.Skeleton, BitmapRes.Wall3, 2);
            wall2.Sprite = spritebox2;

            //The wall to the right of the first door
            cCritterWall wall3 = new cCritterWall(
                new cVector3(16.5f, 0, 5),
                new cVector3(16.5f, 0, 4),
                31f,
                16,
                this);
            cSpriteTextureBox spritebox3 =
                new cSpriteTextureBox(wall3.Skeleton, BitmapRes.Wall3, 2);
            wall3.Sprite = spritebox3;

            /*
             * Not yet finished with these two, so it's commented out for now.
            //The wall to the right after the first door
            cCritterWall wall4 = new cCritterWall(
                new cVector3(2, 3, 5),
                new cVector3(2, 3, 4),
                32,
                7,
                this);
            cSpriteTextureBox spritebox4 =
                new cSpriteTextureBox(wall4.Skeleton, BitmapRes.Wall3, 1);
            wall4.Sprite = spritebox4;

            //the roof above wall4
            cCritterWall wall5 = new cCritterWall(
                new cVector3(0, 3, 5),
                new cVector3(0, 3, 4),
                2,
                16,
                this);
            cSpriteTextureBox spritebox5 =
                new cSpriteTextureBox(wall5.Skeleton, BitmapRes.Wall3, 1);
            wall5.Sprite = spritebox5;
            */
            //the first door
            cCritterDoorLocked door1 = new cCritterDoorLocked(
                new cVector3(0, -8, 4.5f),
                new cVector3(0, -5, 4.5f),
                2, 0.1f, this);
            cSpriteTextureBox doorspritebox1 =
                new cSpriteTextureBox(door1.Skeleton, BitmapRes.Door);
            door1.Sprite = doorspritebox1;



            //the exit to the next room
            cCritterDoor pdwall = new cCritterDoor(
                new cVector3(_border.Lox, _border.Loy, _border.Midz),
                new cVector3(_border.Lox, _border.Midy - 3, _border.Midz),
                0.6f, 2, this);
            cSpriteTextureBox pspritedoor =
                new cSpriteTextureBox(pdwall.Skeleton, BitmapRes.Door);
            pdwall.Sprite = pspritedoor;

            wentThrough = true;
            startNewRoom = Age;
            currentRoom = 1;
        }


        public void setRoomHallway()
        {
            Biota.purgeCritters("cCritterWall");
            Biota.purgeCritters("cCritterTreasure");
            Biota.purgeCritters("cCritter3Dcharacter");
            /* Because our critters inherited directly from cCritter, these following lines
             * had to be put in because out critters aren't deleted in the above line.
             */
            Biota.purgeCritters("cCritterBigHead");
            Biota.purgeCritters("cCritterSailorVenus");
            Biota.purgeCritters("cCritterMiniBot");
            Biota.purgeCritters("cCritterSnake");
            Biota.purgeCritters("cCritterChicken");
            Biota.purgeCritters("cCritterBigHead");
            Biota.purgeCritters("cCritterSailorVenus");
            Biota.purgeCritters("cCritterMiniBot");
            Biota.purgeCritters("cCritterSnake");
            Biota.purgeCritters("cCritterChicken");

            setBorder(15.0f, 20.0f, 110.0f);
            cRealBox3 skeleton = new cRealBox3();
            skeleton.copy(_border);
            setSkyBox(skeleton);
            SkyBox.setSideTexture(cRealBox3.HIX, BitmapRes.Wall1, 2);
            SkyBox.setSideTexture(cRealBox3.LOX, BitmapRes.Wall1, 2);
            SkyBox.setSideTexture(cRealBox3.LOY, BitmapRes.Wall6, 2);
            SkyBox.setSideTexture(cRealBox3.HIZ, BitmapRes.Concrete, 1);
            SkyBox.setSideTexture(cRealBox3.LOZ, BitmapRes.Concrete, 1);
            SkyBox.setSideTexture(cRealBox3.HIY, BitmapRes.Metal1, 1);

            _seedcount = 0;
            Player.setMoveBox(new cRealBox3(15.0f, 20.0f, 110.0f));
            float height = 0.1f * _border.YSize;
            float ycenter = -_border.YRadius + height / 2.0f;
            float wallthickness = cGame3D.WALLTHICKNESS;
            seedCritters();

            //the platform at the top of the first ramp
            cCritterWall pwall = new cCritterWall(
                new cVector3(5.0f, -6, 15.0f),
                new cVector3(5.0f, -6, 20.0f),
                5,
                5,
                this);
            cSpriteTextureBox pspritebox =
                new cSpriteTextureBox(pwall.Skeleton, BitmapRes.Wall3, 1); 
                pwall.Sprite = pspritebox;

            //the ramp near the start
            cCritterWall pwall3 = new cCritterWall(
                new cVector3(5.0f, -5.1f, 18.85f),
                new cVector3(5.0f, -13, 30.0f),
                4.75f,
                4,
                this);
            cSpriteTextureBox pspritebox3 =
                new cSpriteTextureBox(pwall3.Skeleton, BitmapRes.Wall3, 1);
            pwall3.Sprite = pspritebox3;

            //the ramp after the lava
            cCritterWall pwall4 = new cCritterWall(
                new cVector3(0.0f, -4f, -20f),
                new cVector3(0.0f, -13, -40.0f),
                15f,
                5f,
                this);
            cSpriteTextureBox pspritebox4 =
                new cSpriteTextureBox(pwall4.Skeleton, BitmapRes.Wall3, 1);
            pwall4.Sprite = pspritebox4;
            //the wall stopping the player from jumping over the lava onto the ramp
            cCritterWall pwall5 = new cCritterWall(
                new cVector3(0.0f, -4f, -20.0f),
                new cVector3(0.0f, -13, -20.0f),
                15f,
                9f,
                this);
            cSpriteTextureBox pspritebox5 =
                new cSpriteTextureBox(pwall5.Skeleton, BitmapRes.Wall3, 1);
            pwall5.Sprite = pspritebox5;

            cCritterWall pwall6 = new cCritterWall(
                new cVector3(0, -10, 5),
                new cVector3(0, -10, 4),
                15f,
                2,
                this);
            cSpriteTextureBox pspritebox6 =
                new cSpriteTextureBox(pwall6.Skeleton, BitmapRes.Wall3, 1);
            pwall6.Sprite = pspritebox6;

            cCritterLava lava = new cCritterLava(
                new cVector3(0, -10, -25.0f),
                new cVector3(0, -10, 4.0f),
                16,
                1,
                this);
            cSpriteTextureBox lavaspritebox3 =
                new cSpriteTextureBox(lava.Skeleton, BitmapRes.Lava, 1);
            lava.Sprite = lavaspritebox3;

            cCritterWallMoving movingwall = new cCritterWallMoving(
                new cVector3(5.0f, -2, -6.0f),
                new cVector3(5.0f, -2, 4.0f),
                10,
                2,
                this);
            cSpriteTextureBox movingwallspritebox = new cSpriteTextureBox(movingwall.Skeleton, BitmapRes.Wall3, 1);
            movingwall.Sprite = movingwallspritebox;

            cCritterWallMoving movingwall2 = new cCritterWallMoving(
                new cVector3(-5.0f, -2, -10.0f),
                new cVector3(-5.0f, -2, -20.0f),
                7,
                2,
                this);
            cSpriteTextureBox movingwallspritebox2 = new cSpriteTextureBox(movingwall2.Skeleton, BitmapRes.Wall3, 1);
            movingwall2.Sprite = movingwallspritebox2;

            cCritterDoor endDoor = new cCritterDoor(
                new cVector3(0, -10, _border.Loz),
                new cVector3(0, -5, _border.Loz),
                2, 0.6f, this);
            cSpriteTextureBox pspritedoor =
                new cSpriteTextureBox(endDoor.Skeleton, BitmapRes.Door);
            endDoor.Sprite = pspritedoor;

            currentRoom = 2;
            wentThrough = true;
        }
        public void setRoom3()
        {
            Biota.purgeCritters("cCritterWall");
            Biota.purgeCritters("cCritterTreasure");
            Biota.purgeCritters("cCritter3Dcharacter");
            /* Because our critters inherited directly from cCritter, these following lines
             * had to be put in because out critters aren't deleted in the above line.
             */
            Biota.purgeCritters("cCritterBigHead");
            Biota.purgeCritters("cCritterSailorVenus");
            Biota.purgeCritters("cCritterMiniBot");
            Biota.purgeCritters("cCritterSnake");
            Biota.purgeCritters("cCritterChicken");
            Biota.purgeCritters("cCritterBigHead");
            Biota.purgeCritters("cCritterSailorVenus");
            Biota.purgeCritters("cCritterMiniBot");
            Biota.purgeCritters("cCritterSnake");
            Biota.purgeCritters("cCritterChicken");

            setBorder(50.0f, 50.0f, 50.0f);
            cRealBox3 skeleton = new cRealBox3();
            skeleton.copy(_border);
            setSkyBox(skeleton);
            SkyBox.setSideTexture(cRealBox3.HIX, BitmapRes.Wall6, 2);
            SkyBox.setSideTexture(cRealBox3.LOX, BitmapRes.Wall6, 2);
            SkyBox.setSideTexture(cRealBox3.LOY, BitmapRes.Wall6, 2);
            SkyBox.setSideTexture(cRealBox3.HIZ, BitmapRes.Wall6, 2);
            SkyBox.setSideTexture(cRealBox3.LOZ, BitmapRes.Wall6, 2);
            SkyBox.setSideTexture(cRealBox3.HIY, BitmapRes.Metal1, 1);

            _seedcount = 0;
            Player.setMoveBox(new cRealBox3(50.0f, 50.0f, 50.0f));

            float height = 0.1f * _border.YSize;
            float ycenter = -_border.YRadius + height / 2.0f;
            float wallthickness = cGame3D.WALLTHICKNESS;
            Player.moveTo(new cVector3(0.0f, -30.0f ,0.0f)); 

            currentRoom = 3;
            wentThrough = true;

            cCritter3DPlayerHomer player = (cCritter3DPlayerHomer)Player;
            player.resetKillCount();
        }
		
        //The commented out loop causes a bug that prevents the player from jumping properly.
		public override void seedCritters() 
		{
			Biota.purgeCritters( "cCritterBullet" ); 
			Biota.purgeCritters( "cCritter3Dcharacter" );
            //for (int i = 0; i < _seedcount; i++) 
				//new cCritter3Dcharacter( this );
            Player.moveTo(new cVector3(0.0f, Border.Loy, Border.Hiz - 3.0f)); 
				/* We start at hiz and move towards	loz */

            new cCritterBigHead(this);
            new cCritterSailorVenus(this);
            new cCritterMiniBot(this);
            new cCritterSnake(this);
            new cCritterChicken(this);
            new cCritterBigHead(this);
            new cCritterSailorVenus(this);
            new cCritterMiniBot(this);
            new cCritterSnake(this);
            new cCritterChicken(this);
		} 

		
		public void setdoorcollision( ) { doorcollision = true; } 
		
		public override ACView View 
		{
            set
            {
                base.View = value; //You MUST call the base class method here.
                value.setUseBackground(ACView.FULL_BACKGROUND); /* The background type can be
			    ACView.NO_BACKGROUND, ACView.SIMPLIFIED_BACKGROUND, or 
			    ACView.FULL_BACKGROUND, which often means: nothing, lines, or
			    planes&bitmaps, depending on how the skybox is defined. */
                value.pviewpointcritter().Listener = new cListenerViewerRide();
            }
		} 

		
		public override cCritterViewer Viewpoint 
		{ 
            set
            {
			    if ( value.Listener.RuntimeClass == "cListenerViewerRide" ) 
			    { 
				    value.setViewpoint( new cVector3( 0.0f, 0.3f, -1.0f ), _border.Center); 
					//Always make some setViewpoint call simply to put in a default zoom.
				    value.zoom( 0.35f ); //Wideangle 
				    cListenerViewerRide prider = ( cListenerViewerRide )( value.Listener); 
				    prider.Offset = (new cVector3( -1.5f, 0.0f, 1.0f)); /* This offset is in the coordinate
				    system of the player, where the negative X axis is the negative of the
				    player's tangent direction, which means stand right behind the player. */ 
			    } 
			    else //Not riding the player.
			    { 
				    value.zoom( 1.0f ); 
				    /* The two args to setViewpoint are (directiontoviewer, lookatpoint).
				    Note that directiontoviewer points FROM the origin TOWARDS the viewer. */ 
				    value.setViewpoint( new cVector3( 0.0f, 0.3f, 1.0f ), _border.Center); 
			    }
            }
		} 

		/* Move over to be above the
			lower left corner where the player is.  In 3D, use a low viewpoint low looking up. */ 
	
		public override void adjustGameParameters() 
		{
		// (1) End the game if the player is dead 
			if ( (Health == 0) && !_gameover ) //Player's been killed and game's not over.
			{ 
				_gameover = true; 
				Player.addScore( _scorecorrection ); // So user can reach _maxscore  
                Framework.snd.play(Sound.Hallelujah);
                return ; 
			} 
		// (2) Also don't let the the model count diminish.
					//(need to recheck propcount in case we just called seedCritters).
			//int modelcount = Biota.count( "cCritter3Dcharacter" ); 
			//int modelstoadd = _seedcount - modelcount; 
			//for ( int i = 0; i < modelstoadd; i++) 
				//new cCritter3Dcharacter( this ); 
		// (3) Maybe check some other conditions.

            if (wentThrough && (Age - startNewRoom) > 2.0f)
            {
                //MessageBox.Show("What an idiot.");
                wentThrough = false;
            }

            if (currentRoom == 3)
            {
                //it's hard to believe that adjustGameParameters doesn't have dt already passed into it.
                timeToSpawn -= Framework.pdoc.getdt(); 

                if (timeToSpawn <=0)
                {
                    //create more critters to fight.
                    //I can't use seedCritters because it moves the player to the start.
                    //I used randomize position because they need to spawn anywhere rather than
                    //just one end of the room.
                    cCritterBigHead a = new cCritterBigHead(this);
                    a.randomizePosition();
                    a.moveTo(a.Position.add(new cVector3(0.0f, 50.0f, 0.0f)));

                    cCritterSailorVenus b = new cCritterSailorVenus(this);
                    b.randomizePosition();
                    b.moveTo(a.Position.add(new cVector3(0.0f, 50.0f, 0.0f)));

                    cCritterMiniBot c= new cCritterMiniBot(this);
                    c.randomizePosition();
                    c.moveTo(a.Position.add(new cVector3(0.0f, 50.0f, 0.0f)));

                    cCritterSnake d = new cCritterSnake(this);
                    d.randomizePosition();
                    d.moveTo(a.Position.add(new cVector3(0.0f, 50.0f, 0.0f)));

                    cCritterChicken e = new cCritterChicken(this);
                    e.randomizePosition();
                    e.moveTo(a.Position.add(new cVector3(0.0f, 50.0f, 0.0f)));

                    timeToSpawn = 5.0f;
                }
                //If the player has made 20 kills in room 3, create the door for the player to go through.
                cCritter3DPlayerHomer player = (cCritter3DPlayerHomer)Player;
                if (player.getKillCount()>=20 && createdDoor==false)
                {
                    createdDoor = true;
                    cCritterDoor endDoor = new cCritterDoor(
                        new cVector3(0, -25, _border.Loz),
                        new cVector3(0, -20, _border.Loz),
                        2, 0.6f, this);
                    cSpriteTextureBox pspritedoor =
                        new cSpriteTextureBox(endDoor.Skeleton, BitmapRes.Door);
                    endDoor.Sprite = pspritedoor;
                }

                //Create a health pack at a random location when the player has made 5, 10, and 15 kills.
                //createdHealth keeps track of how many health packs have already been made.
                if ((player.getKillCount() >= 5 && createdHealth == 0) ||
                    (player.getKillCount() >= 10 && createdHealth == 1) ||
                    (player.getKillCount() >= 15 && createdHealth == 2))
                {
                    createdHealth += 1;

                    //the + and - 2 are to prevent the healthpack from spawning in the border.
                    float randomx = Framework.randomOb.randomReal(_border.Lox+2, _border.Hix-2);
                    float randomz = Framework.randomOb.randomReal(_border.Loz+2, _border.Hiz-2);

                    cCritterHealth phealth = new cCritterHealth(
                        new cVector3(randomx, -24, randomz),
                        new cVector3(randomx, -24, randomz + 2.0f),
                        2,
                        2,
                        this);
                    cSpriteTextureBox healthspritebox = new cSpriteTextureBox(phealth.Skeleton, BitmapRes.Health, 1);
                    phealth.Sprite = healthspritebox;
                }
            }

            if (doorcollision == true)
            {
                if (currentRoom == 1)
                {
                    setRoomHallway();
                }
                else if (currentRoom == 2)
                {
                    setRoom3();
                }
                else if (currentRoom ==3 && _win==false )
                {
                    _gameover = true;
                    Player.addScore(500);
                    _win = true;
                    
                    Framework.snd.play(Sound.Hallelujah);
                    return; 
                }
                doorcollision = false;
            }
		} 
		
		public override bool upIsZ(){ return false; } 
		
	}

    class cCritterSailorVenus : cCritter
    {
        public bool Dead { get; private set; }
        public bool KilledByPlayer { get; set; }

        public cCritterSailorVenus(cGame pownergame)
            : base(pownergame)
        {
            addForce(new cForceGravity(25.0f, new cVector3(0.0f, -1, 0.00f)));
            addForce(new cForceDrag(20.0f));  // default friction strength 0.5 
            Density = 2.0f;
            MaxSpeed = 30.0f;
            if (pownergame != null) //Just to be safe.
                Sprite = new cSpriteQuake(ModelsMD2.SailorVenus);

            if (Sprite.IsKindOf("cSpriteQuake")) //Don't let the figurines tumble.  
            {
                //Stops cSpriteQuake from tumbling.
                AttitudeToMotionLock = false;
                Attitude = new cMatrix3(new cVector3(0.0f, 0.0f, 1.0f),
                    new cVector3(1.0f, 0.0f, 0.0f),
                    new cVector3(0.0f, 1.0f, 0.0f), Position);
            }

            /* Orient them so they are facing towards positive Z with heads towards Y. */
            Bounciness = 0.0f; //Not 1.0 means it loses a bit of energy with each bounce.
            setRadius(0.5f);
            MinTwitchThresholdSpeed = 4.0f; //Means sprite doesn't switch direction unless it's moving fast 
            randomizePosition(new cRealBox3(new cVector3(_movebox.Lox, _movebox.Loy, _movebox.Loz + 4.0f),
                new cVector3(_movebox.Hix, _movebox.Loy, _movebox.Midz - 1.0f)));
            /* I put them ahead of the player  */
            randomizeVelocity(0.0f, 30.0f, false);

            Sprite.ModelState = State.Idle;

            _wrapflag = cCritter.BOUNCE;
        }

        public override void update(ACView pactiveview, float dt)
        {
            base.update(pactiveview, dt);

            if (!Dead)
            {
                rotateAttitude(Tangent.rotationAngle(AttitudeTangent));

                float playerDistance = distanceTo(Player);
                if (playerDistance > 20)
                {
                    Sprite.ModelState = State.Idle;
                    clearForcelist();
                    addForce(new cForceGravity(25.0f, new cVector3(0.0f, -1, 0.00f)));
                    addForce(new cForceDrag(5.0f));  // default friction strength 0.5 
                }
                else if (playerDistance > 1.5)
                {
                    Sprite.ModelState = State.Run;
                    clearForcelist();
                    addForce(new cForceGravity(25.0f, new cVector3(0.0f, -1, 0.00f)));
                    addForce(new cForceDrag(5.0f));  // default friction strength 0.5 
                    addForce(new cForceObjectSeek(Player, 35.0f));
                }
                else
                {
                    Sprite.setstate(State.Other, 112, 134, StateType.Repeat);
                    clearForcelist();
                    addForce(new cForceGravity(25.0f, new cVector3(0.0f, -1, 0.00f)));
                    addForce(new cForceDrag(5.0f));  // default friction strength 0.5 
                    addForce(new cForceObjectSeek(Player, 0.1f));
                }
            }
            //if ((_outcode & cRealBox3.BOX_HIZ) != 0) /* use bitwise AND to check if a flag is set. */
            //delete_me(); //tell the game to remove yourself if you fall up to the hiz.
         
        } 

        public override void die()
        {
            if (!Dead)
            {
                Dead = true;

                if (KilledByPlayer)
                {
                    Player.addScore(Value);
                    cCritter3DPlayerHomer player = (cCritter3DPlayerHomer)Player;
                    player.increaseKillCount();
                }

                clearForcelist();
                addForce(new cForceGravity(25.0f, new cVector3(0.0f, -1, 0.00f)));
                addForce(new cForceDrag(20.0f));  // default friction strength 0.5 
                Sprite.setstate(State.Other, 190, 197, StateType.Hold);
            }
        } 


        public override bool IsKindOf(string str)
        {
            return str == "cCritterSailorVenus" || base.IsKindOf(str);
        }

        public override string RuntimeClass
        {
            get
            {
                return "cCritterSailorVenus";
            }
        }
    }

    class cCritterBigHead : cCritter
    {
        public bool Dead { get; private set; }
        public bool KilledByPlayer { get; set; }

        public cCritterBigHead(cGame pownergame)
            : base(pownergame)
        {
            addForce(new cForceGravity(25.0f, new cVector3(0.0f, -1, 0.00f)));
            addForce(new cForceDrag(20.0f));  // default friction strength 0.5 
            Density = 2.0f;
            MaxSpeed = 30.0f;
            if (pownergame != null) //Just to be safe.
                Sprite = new cSpriteQuake(ModelsMD2.BigHead);

            if (Sprite.IsKindOf("cSpriteQuake")) //Don't let the figurines tumble.  
            {
                //Stops cSpriteQuake from tumbling.
                AttitudeToMotionLock = false;
                Attitude = new cMatrix3(new cVector3(0.0f, 0.0f, 1.0f),
                    new cVector3(1.0f, 0.0f, 0.0f),
                    new cVector3(0.0f, 1.0f, 0.0f), Position);

            }
            /* Orient them so they are facing towards positive Z with heads towards Y. */
            Bounciness = 0.0f; //Not 1.0 means it loses a bit of energy with each bounce.
            setRadius(0.5f);
            MinTwitchThresholdSpeed = 4.0f; //Means sprite doesn't switch direction unless it's moving fast 
            randomizePosition(new cRealBox3(new cVector3(_movebox.Lox, _movebox.Loy, _movebox.Loz + 4.0f),
                new cVector3(_movebox.Hix, _movebox.Loy, _movebox.Midz - 1.0f)));
            /* I put them ahead of the player  */
            randomizeVelocity(0.0f, 30.0f, false);

            Sprite.ModelState = State.Idle;

            _wrapflag = cCritter.BOUNCE;
        }

        public override void update(ACView pactiveview, float dt)
        {
            base.update(pactiveview, dt);

            if (!Dead)
            {
                rotateAttitude(Tangent.rotationAngle(AttitudeTangent));

                float playerDistance = distanceTo(Player);
                if (playerDistance > 1.5)
                {
                    Sprite.ModelState = State.Run;
                    clearForcelist();
                    addForce(new cForceGravity(25.0f, new cVector3(0.0f, -1, 0.00f)));
                    addForce(new cForceDrag(5.0f));  // default friction strength 0.5 
                    addForce(new cForceObjectSeek(Player, 25.0f));
                }
                else
                {
                    Sprite.setstate(State.Other, 112, 134, StateType.Repeat);
                    clearForcelist();
                    addForce(new cForceGravity(25.0f, new cVector3(0.0f, -1, 0.00f)));
                    addForce(new cForceDrag(5.0f));  // default friction strength 0.5 
                    addForce(new cForceObjectSeek(Player, 0.1f));
                }
            }
            //if ((_outcode & cRealBox3.BOX_HIZ) != 0) /* use bitwise AND to check if a flag is set. */
                //delete_me(); //tell the game to remove yourself if you fall up to the hiz.

        }

        public override void die()
        {
            if (!Dead)
            {
                Dead = true;

                if (KilledByPlayer)
                {
                    Player.addScore(100);
                    cCritter3DPlayerHomer player = (cCritter3DPlayerHomer)Player;
                    player.increaseKillCount();
                }

                clearForcelist();
                addForce(new cForceGravity(25.0f, new cVector3(0.0f, -1, 0.00f)));
                addForce(new cForceDrag(20.0f));  // default friction strength 0.5 
                Sprite.setstate(State.Other, 190, 197, StateType.Hold);
            }
        }


        public override bool IsKindOf(string str)
        {
            return str == "cCritterBigHead" || base.IsKindOf(str);
        }

        public override string RuntimeClass
        {
            get
            {
                return "cCritterBigHead";
            }
        }
    }

    class cCritterSnake : cCritterArmed
    {
        public bool Dead { get; private set; }
        public bool KilledByPlayer { get; set; }

        public cCritterSnake(cGame pownergame)
            : base(pownergame)
        {
            WaitShoot = 1f;
            _bshooting = true;
            Density = 2.0f;
            MaxSpeed = 30.0f;
            BulletClass = new cCritterBulletPoison();
            
            addForce(new cForceGravity(25.0f, new cVector3(0.0f, -1, 0.00f)));
            addForce(new cForceDrag(20.0f));  // default friction strength 0.5 
                    

            if (pownergame != null) //Just to be safe.
                Sprite = new cSpriteQuake(ModelsMD2.Cobra);
            

            //Stops cSpriteQuake from tumbling.
            AttitudeToMotionLock = false;
            Attitude = new cMatrix3(new cVector3(0.0f, 0.0f, 1.0f),
                new cVector3(1.0f, 0.0f, 0.0f),
                new cVector3(0.0f, 1.0f, 0.0f), Position);

            /* Orient them so they are facing towards positive Z with heads towards Y. */
            Bounciness = 0.0f; //Not 1.0 means it loses a bit of energy with each bounce.
            setRadius(1.0f);
            MinTwitchThresholdSpeed = 4.0f; //Means sprite doesn't switch direction unless it's moving fast 
            randomizePosition(new cRealBox3(new cVector3(_movebox.Lox, _movebox.Loy, _movebox.Loz + 4.0f),
                new cVector3(_movebox.Hix, _movebox.Loy, _movebox.Midz - 1.0f)));
            /* I put them ahead of the player  */
            randomizeVelocity(0.0f, 30.0f, false);

            Sprite.ModelState = State.Idle;

            _wrapflag = cCritter.BOUNCE;
        }
        

        public override void update(ACView pactiveview, float dt)
        {
            base.update(pactiveview, dt);

            if (!Dead)
            {
                rotateAttitude(Tangent.rotationAngle(AttitudeTangent));


                float playerDistance = distanceTo(Player);
                if (playerDistance > 20)
                {
                    Sprite.ModelState = State.Idle;
                    clearForcelist();
                    addForce(new cForceGravity(25.0f, new cVector3(0.0f, -1, 0.00f)));
                    addForce(new cForceDrag(5.0f));  // default friction strength 0.5 
                }
                else if (playerDistance > 10)
                {
                    aimAt(_ptarget);
                    //(2) Align gun with move direction if necessary.
                    if (_aimtoattitudelock)
                        AimVector = AttitudeTangent; /* Keep the gun pointed in the right direction. */
                    //(3) Shoot if possible.
                    if (!_armed || !_bshooting)
                        return;
                    /* If _age has been reset to 0.0, you need to get ageshoot back in synch. */
                    if (_age < _ageshoot)
                        _ageshoot = _age;
                    if ((_age - _ageshoot > _waitshoot)) //A shoot key is down 
                    {

                        shoot();
                        _ageshoot = _age;
                    }
                }
                else if (playerDistance > 1.5)
                {
                    Sprite.ModelState = State.Run;
                    clearForcelist();
                    addForce(new cForceGravity(25.0f, new cVector3(0.0f, -1, 0.00f)));
                    addForce(new cForceDrag(5.0f));  // default friction strength 0.5 
                    addForce(new cForceObjectSeek(Player, 20.0f));

                    aimAt(_ptarget);
                    //(2) Align gun with move direction if necessary.
                    if (_aimtoattitudelock)
                        AimVector = AttitudeTangent; /* Keep the gun pointed in the right direction. */
                    //(3) Shoot if possible.
                    if (!_armed || !_bshooting)
                        return;
                    /* If _age has been reset to 0.0, you need to get ageshoot back in synch. */
                    if (_age < _ageshoot)
                        _ageshoot = _age;
                    if ((_age - _ageshoot > _waitshoot)) //A shoot key is down 
                    {

                        shoot();
                        _ageshoot = _age;
                    }
                }
                else
                {
                    Sprite.setstate(State.Other, 112, 134, StateType.Repeat);
                    clearForcelist();
                    addForce(new cForceGravity(25.0f, new cVector3(0.0f, -1, 0.00f)));
                    addForce(new cForceDrag(5.0f));  // default friction strength 0.5 
                    addForce(new cForceObjectSeek(Player, 0.1f));
                }
            }

            //if ((_outcode & cRealBox3.BOX_HIZ) != 0) /* use bitwise AND to check if a flag is set. */
            //delete_me(); //tell the game to remove yourself if you fall up to the hiz.      
        } 

        

        public override void die()
        {
            if (!Dead)
            {
                _bshooting = false;
                Dead = true;

                if (KilledByPlayer)
                {
                    Player.addScore(Value);
                    cCritter3DPlayerHomer player = (cCritter3DPlayerHomer)Player;
                    player.increaseKillCount();
                }

                clearForcelist();
                addForce(new cForceGravity(25.0f, new cVector3(0.0f, -1, 0.00f)));
                addForce(new cForceDrag(20.0f));  // default friction strength 0.5 
                Sprite.setstate(State.Other, 190, 197, StateType.Hold);
            }
        }

        
        public override bool IsKindOf(string str)
        {
            return str == "cCritterSnake" || base.IsKindOf(str);
        }

        public override string RuntimeClass
        {
            get
            {
                return "cCritterSnake";
            }
        }
      
        

        public override float WaitShoot
        {
            set
            {
                _waitshoot = value;
                _ageshoot = _age - Framework.randomOb.randomReal(0.0f, _waitshoot);
                /* Do this so they don't all shoot at once,	when you have several of them. */
            }
        }

        public override float Age
        {
            set
            {
                base.Age = value;   // will call cCritter setAge
                WaitShoot = _waitshoot;
            }
        }

     }

    class cCritterChicken : cCritterArmed
    {
        public bool Dead { get; private set; }
        public bool KilledByPlayer { get; set; }

        public cCritterChicken(cGame pownergame)
            : base(pownergame)
        {
            
            WaitShoot = 1f;
            _bshooting = true;
            Density = 2.0f;
            MaxSpeed = 30.0f;
            BulletClass = new cCritterBulletEggs();
            //addForce(new cForceGravity(25.0f, new cVector3(0.0f, -1, 0.00f)));
            addForce(new cForceDrag(20.0f));  // default friction strength 0.5 
            
            if (pownergame != null) //Just to be safe.
                Sprite = new cSpriteQuake(ModelsMD2.Chicken);

            if (Sprite.IsKindOf("cSpriteQuake")) //Don't let the figurines tumble.  
            {
                //Stops cSpriteQuake from tumbling.
                AttitudeToMotionLock = false;
                Attitude = new cMatrix3(new cVector3(0.0f, 0.0f, 1.0f),
                    new cVector3(1.0f, 0.0f, 0.0f),
                    new cVector3(0.0f, 1.0f, 0.0f), Position);

            }
            /* Orient them so they are facing towards positive Z with heads towards Y. */
            Bounciness = 0.0f; //Not 1.0 means it loses a bit of energy with each bounce.
            setRadius(0.3f);
            MinTwitchThresholdSpeed = 4.0f; //Means sprite doesn't switch direction unless it's moving fast 
            randomizePosition(new cRealBox3(new cVector3(_movebox.Lox, _movebox.Loy, _movebox.Loz + 4.0f),
                new cVector3(_movebox.Hix, _movebox.Hiy - 1f, _movebox.Midz - 1.0f)));
            /* I put them ahead of the player  */
            randomizeVelocity(0.0f, 30.0f, false);
            
            Sprite.ModelState = State.Idle;

            _wrapflag = cCritter.BOUNCE;
        }

        public override void update(ACView pactiveview, float dt)
        {
            base.update(pactiveview, dt);

            if (!Dead)
            {
                rotateAttitude(Tangent.rotationAngle(AttitudeTangent));

                float playerDistance = distanceTo(Player);
                if (playerDistance > 50)
                {
                    Sprite.ModelState = State.Idle;
                    clearForcelist();
                    //addForce(new cForceGravity(25.0f, new cVector3(0.0f, -1, 0.00f)));
                    addForce(new cForceDrag(5.0f));  // default friction strength 0.5 
                }
                else if (playerDistance > 1.5)
                {
                    Sprite.ModelState = State.Run;
                    clearForcelist();
                    //addForce(new cForceGravity(25.0f, new cVector3(0.0f, -1, 0.00f)));
                    addForce(new cForceDrag(5.0f));  // default friction strength 0.5 
                    addForce(new cForceFlyingObjectSeek(Player, 40.0f));
                }
                else
                {
                    Sprite.setstate(State.Other, 112, 134, StateType.Repeat);
                    clearForcelist();
                    //addForce(new cForceGravity(25.0f, new cVector3(0.0f, -1, 0.00f)));
                    addForce(new cForceDrag(5.0f));  // default friction strength 0.5 
                    addForce(new cForceFlyingObjectSeek(Player, 0.1f));
                }

                aimAt(_ptarget);

                //(2) Align gun with move direction if necessary.
                if (_aimtoattitudelock)
                    AimVector = AttitudeTangent; /* Keep the gun pointed in the right direction. */
                //(3) Shoot if possible.
                if (!_armed || !_bshooting)
                    return;
                /* If _age has been reset to 0.0, you need to get ageshoot back in synch. */
                if (_age < _ageshoot)
                    _ageshoot = _age;
                if ((_age - _ageshoot > _waitshoot)) //A shoot key is down 
                {

                    shoot();
                    _ageshoot = _age;
                }
            }
            //if ((_outcode & cRealBox3.BOX_HIZ) != 0) /* use bitwise AND to check if a flag is set. */
            //delete_me(); //tell the game to remove yourself if you fall up to the hiz.

        }

        public override void die()
        {
            if (!Dead)
            {
                _bshooting = false;
                Dead = true;

                if (KilledByPlayer)
                {
                    Player.addScore(Value);
                    cCritter3DPlayerHomer player = (cCritter3DPlayerHomer)Player;
                    player.increaseKillCount();
                }

                clearForcelist();
                addForce(new cForceGravity(25.0f, new cVector3(0.0f, -1, 0.00f)));
                addForce(new cForceDrag(20.0f));  // default friction strength 0.5 
                Sprite.setstate(State.Other, 190, 197, StateType.Hold);
            }
        }

        public override float WaitShoot
        {
            set
            {
                _waitshoot = value;
                _ageshoot = _age - Framework.randomOb.randomReal(0.0f, _waitshoot);
                /* Do this so they don't all shoot at once,	when you have several of them. */
            }
        }

        public override float Age
        {
            set
            {
                base.Age = value;   // will call cCritter setAge
                WaitShoot = _waitshoot;
            }
        }


        public override bool IsKindOf(string str)
        {
            return str == "cCritterChicken" || base.IsKindOf(str);
        }

        public override string RuntimeClass
        {
            get
            {
                return "cCritterChicken";
            }
        }

    }

    class cCritterMiniBot : cCritter
    {
        public bool Dead { get; private set; }
        public bool KilledByPlayer { get; set; }

        public cCritterMiniBot(cGame pownergame)
            : base(pownergame)
        {
            addForce(new cForceGravity(25.0f, new cVector3(0.0f, -1, 0.00f)));
            addForce(new cForceDrag(20.0f));  // default friction strength 0.5 
            Density = 2.0f;
            MaxSpeed = 30.0f;
            if (pownergame != null) //Just to be safe.
                Sprite = new cSpriteQuake(ModelsMD2.MiniBot);

            if (Sprite.IsKindOf("cSpriteQuake")) //Don't let the figurines tumble.  
            {
                //Stops cSpriteQuake from tumbling.
                AttitudeToMotionLock = false;
                Attitude = new cMatrix3(new cVector3(0.0f, 0.0f, 1.0f),
                    new cVector3(1.0f, 0.0f, 0.0f),
                    new cVector3(0.0f, 1.0f, 0.0f), Position);

            }
            /* Orient them so they are facing towards positive Z with heads towards Y. */
            Bounciness = 0.0f; //Not 1.0 means it loses a bit of energy with each bounce.
            setRadius(1.5f);
            MinTwitchThresholdSpeed = 4.0f; //Means sprite doesn't switch direction unless it's moving fast 
            randomizePosition(new cRealBox3(new cVector3(_movebox.Lox, _movebox.Loy, _movebox.Loz + 4.0f),
                new cVector3(_movebox.Hix, _movebox.Loy, _movebox.Midz - 1.0f)));
            /* I put them ahead of the player  */
            randomizeVelocity(0.0f, 30.0f, false);

            Sprite.ModelState = State.Idle;

            _wrapflag = cCritter.BOUNCE;
        }

        public override void update(ACView pactiveview, float dt)
        {
            base.update(pactiveview, dt);

            if (!Dead)
            {
                rotateAttitude(Tangent.rotationAngle(AttitudeTangent));

                float playerDistance = distanceTo(Player);
                if (playerDistance > 25)
                {
                    Sprite.ModelState = State.Idle;
                    clearForcelist();
                    addForce(new cForceGravity(25.0f, new cVector3(0.0f, -1, 0.00f)));
                    addForce(new cForceDrag(5.0f));  // default friction strength 0.5 
                }
                else if (playerDistance > 1.5)
                {
                    Sprite.ModelState = State.Run;
                    clearForcelist();
                    addForce(new cForceGravity(25.0f, new cVector3(0.0f, -1, 0.00f)));
                    addForce(new cForceDrag(5.0f));  // default friction strength 0.5 
                    addForce(new cForceObjectSeek(Player, 15.0f));
                }
                else
                {
                    Sprite.setstate(State.Other, 112, 134, StateType.Repeat);
                    clearForcelist();
                    addForce(new cForceGravity(25.0f, new cVector3(0.0f, -1, 0.00f)));
                    addForce(new cForceDrag(5.0f));  // default friction strength 0.5 
                    addForce(new cForceObjectSeek(Player, 0.1f));
                }
            }
            //if ((_outcode & cRealBox3.BOX_HIZ) != 0) /* use bitwise AND to check if a flag is set. */
            //delete_me(); //tell the game to remove yourself if you fall up to the hiz.

        }

        public override void die()
        {
            if (!Dead)
            {
                Dead = true;

                if (KilledByPlayer)
                {
                    Player.addScore(Value);
                    cCritter3DPlayerHomer player = (cCritter3DPlayerHomer)Player;
                    player.increaseKillCount();
                }

                clearForcelist();
                addForce(new cForceGravity(25.0f, new cVector3(0.0f, -1, 0.00f)));
                addForce(new cForceDrag(20.0f));  // default friction strength 0.5 
                Sprite.setstate(State.Other, 190, 197, StateType.Hold);
            }
        }

        public override bool IsKindOf(string str)
        {
            return str == "cCritterMiniBot" || base.IsKindOf(str);
        }

        public override string RuntimeClass
        {
            get
            {
                return "cCritterMiniBot";
            }
        }
    }
}