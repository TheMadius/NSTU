#include "human.h"

Human::Human(bool gender,QObject *parent): ContrlObject(parent)
{
    this->m_gender = gender;

    this->init();

    this->m_myConnect = connect(m_gameTimer,&QTimer::timeout,this,&Human::contrlNPS);

    setRotation(0);
}

 QList<Disease> Human::getlistDisease()
 {
     return this->listDisease;
 }

void Human::delDisease(Disease ill)
{
     if(this->listDisease.removeOne(ill))
     {
         this->m_step =  this->m_step/ill.getEffectOnSpeed();
     }


     if(this->listDisease.empty())
     {
         disconnect(this->m_ConnectHeal);

         if (m_InjectorTimer != nullptr)
         {
                delete m_InjectorTimer;
               m_InjectorTimer= nullptr;
         }

     }

}

void Human::addDisease(Disease ill)
{
    if(this->listDisease.empty())
    {
        disconnect(this->m_ConnectHeal);

        if (m_InjectorTimer != nullptr)
        {
               delete m_InjectorTimer;
              m_InjectorTimer= nullptr;
        }

        m_InjectorTimer = new QTimer();

        m_InjectorTimer->start(7000);

        m_ConnectHeal = connect(m_InjectorTimer,&QTimer::timeout,this,&Human::isHealthy);
    }

    if (!this->listDisease.contains(ill))
    {
        this->listDisease.append(ill);

        this->m_step =  this->m_step*ill.getEffectOnSpeed();
    }

}

void Human::init()
{
    this->m_gameTimer = nullptr;
    this->m_birthTimer = nullptr;
    this->m_gameTimer = new QTimer();
    this->m_stepNPS = 0;
    this->m_stepBack = 0;
    this->m_steps = 0;
    this->hp = 100;
    m_InjectorTimer = nullptr;
    m_alive = true;
    this->m_countForSteps= 0;
    this->m_ability = true;

    m_gameTimer->start(40);

    if (this->m_gender)
    {
        this->initMan();
    }
    else
    {
        this->initFemale();
    }

    this->m_widthPNG =  this->m_animation[0][0]->width();
    this->m_heightPNG = this->m_animation[0][0]->height();

}

void Human::initMan()
{
    //up
    this->m_animation[0][0] = new QPixmap(":/resourse/Man/images/Sprite_claudius_07.png");
    this->m_animation[0][1] = new QPixmap(":/resourse/Man/images/Sprite_claudius_08.png");
    this->m_animation[0][2] = new QPixmap(":/resourse/Man/images/Sprite_claudius_09.png");
    this->m_animation[0][3] = new QPixmap(":/resourse/Man/images/Sprite_claudius_10.png");
    this->m_animation[0][4] = new QPixmap(":/resourse/Man/images/Sprite_claudius_11.png");
    this->m_animation[0][5] = new QPixmap(":/resourse/Man/images/Sprite_claudius_12.png");
    this->m_animation[0][6] = new QPixmap(":/resourse/Man/images/Sprite_claudius_08.png");
    //down
    this->m_animation[1][0] = new QPixmap(":/resourse/Man/images/Sprite_claudius_01.png");
    this->m_animation[1][1] = new QPixmap(":/resourse/Man/images/Sprite_claudius_02.png");
    this->m_animation[1][2] = new QPixmap(":/resourse/Man/images/Sprite_claudius_03.png");
    this->m_animation[1][3] = new QPixmap(":/resourse/Man/images/Sprite_claudius_04.png");
    this->m_animation[1][4] = new QPixmap(":/resourse/Man/images/Sprite_claudius_05.png");
    this->m_animation[1][5] = new QPixmap(":/resourse/Man/images/Sprite_claudius_06.png");
    this->m_animation[1][6] = new QPixmap(":/resourse/Man/images/Sprite_claudius_02.png");
    //right
    this->m_animation[2][0] = new QPixmap(":/resourse/Man/images/Sprite_claudius_19.png");
    this->m_animation[2][1] = new QPixmap(":/resourse/Man/images/Sprite_claudius_20.png");
    this->m_animation[2][2] = new QPixmap(":/resourse/Man/images/Sprite_claudius_21.png");
    this->m_animation[2][3] = new QPixmap(":/resourse/Man/images/Sprite_claudius_22.png");
    this->m_animation[2][4] = new QPixmap(":/resourse/Man/images/Sprite_claudius_23.png");
    this->m_animation[2][5] = new QPixmap(":/resourse/Man/images/Sprite_claudius_24.png");
    this->m_animation[2][6] = new QPixmap(":/resourse/Man/images/Sprite_claudius_20.png");
    //left
    this->m_animation[3][0] = new QPixmap(":/resourse/Man/images/Sprite_claudius_13.png");
    this->m_animation[3][1] = new QPixmap(":/resourse/Man/images/Sprite_claudius_14.png");
    this->m_animation[3][2] = new QPixmap(":/resourse/Man/images/Sprite_claudius_15.png");
    this->m_animation[3][3] = new QPixmap(":/resourse/Man/images/Sprite_claudius_16.png");
    this->m_animation[3][4] = new QPixmap(":/resourse/Man/images/Sprite_claudius_17.png");
    this->m_animation[3][5] = new QPixmap(":/resourse/Man/images/Sprite_claudius_18.png");
    this->m_animation[3][6] = new QPixmap(":/resourse/Man/images/Sprite_claudius_14.png");

}

void Human::initFemale()
{
    //up
    this->m_animation[0][0] = new QPixmap(":/resourse/women/images/Sprite_laila_07.png");
    this->m_animation[0][1] = new QPixmap(":/resourse/women/images/Sprite_laila_08.png");
    this->m_animation[0][2] = new QPixmap(":/resourse/women/images/Sprite_laila_09.png");
    this->m_animation[0][3] = new QPixmap(":/resourse/women/images/Sprite_laila_10.png");
    this->m_animation[0][4] = new QPixmap(":/resourse/women/images/Sprite_laila_11.png");
    this->m_animation[0][5] = new QPixmap(":/resourse/women/images/Sprite_laila_12.png");
    this->m_animation[0][6] = new QPixmap(":/resourse/women/images/Sprite_laila_08.png");
    //down
    this->m_animation[1][0] = new QPixmap(":/resourse/women/images/Sprite_laila_01.png");
    this->m_animation[1][1] = new QPixmap(":/resourse/women/images/Sprite_laila_02.png");
    this->m_animation[1][2] = new QPixmap(":/resourse/women/images/Sprite_laila_03.png");
    this->m_animation[1][3] = new QPixmap(":/resourse/women/images/Sprite_laila_04.png");
    this->m_animation[1][4] = new QPixmap(":/resourse/women/images/Sprite_laila_05.png");
    this->m_animation[1][5] = new QPixmap(":/resourse/women/images/Sprite_laila_06.png");
    this->m_animation[1][6] = new QPixmap(":/resourse/women/images/Sprite_laila_02.png");
    //right
    this->m_animation[2][0] = new QPixmap(":/resourse/women/images/Sprite_laila_19.png");
    this->m_animation[2][1] = new QPixmap(":/resourse/women/images/Sprite_laila_20.png");
    this->m_animation[2][2] = new QPixmap(":/resourse/women/images/Sprite_laila_21.png");
    this->m_animation[2][3] = new QPixmap(":/resourse/women/images/Sprite_laila_22.png");
    this->m_animation[2][4] = new QPixmap(":/resourse/women/images/Sprite_laila_23.png");
    this->m_animation[2][5] = new QPixmap(":/resourse/women/images/Sprite_laila_24.png");
    this->m_animation[2][6] = new QPixmap(":/resourse/women/images/Sprite_laila_20.png");
    //left
    this->m_animation[3][0] = new QPixmap(":/resourse/women/images/Sprite_laila_13.png");
    this->m_animation[3][1] = new QPixmap(":/resourse/women/images/Sprite_laila_14.png");
    this->m_animation[3][2] = new QPixmap(":/resourse/women/images/Sprite_laila_14.png");
    this->m_animation[3][3] = new QPixmap(":/resourse/women/images/Sprite_laila_15.png");
    this->m_animation[3][4] = new QPixmap(":/resourse/women/images/Sprite_laila_16.png");
    this->m_animation[3][5] = new QPixmap(":/resourse/women/images/Sprite_laila_17.png");
    this->m_animation[3][6] = new QPixmap(":/resourse/women/images/Sprite_laila_14.png");

}

QRectF Human::boundingRect() const
{
    return QRectF(-(m_widthPNG/2),-(m_heightPNG/2),m_widthPNG,m_heightPNG);
}

QPainterPath Human::shape() const
{
    QRectF re = boundingRect();

    QPainterPath path;
    path.addEllipse(re);
    return path;
}

void Human::setMovement(bool bMovement)
{

    disconnect(m_myConnect);
    if (m_gameTimer != nullptr)
         delete m_gameTimer;

    this->m_gameTimer = new QTimer();

 if (bMovement)
 {
     m_myConnect = connect(m_gameTimer, &QTimer::timeout, this, &Human::contrPlayer);
     m_gameTimer->start(20);
  }
 else
 {
    this->m_myConnect = connect(m_gameTimer,&QTimer::timeout,this,&Human::contrlNPS);
    m_gameTimer->start(40);

 }
}

void Human::paint(QPainter *painter, const QStyleOptionGraphicsItem *option, QWidget *widget)
{
        painter->drawPixmap(-(m_widthPNG/2),-(m_heightPNG/2),*m_animation[m_direction][m_steps]);

        Q_UNUSED(option);
        Q_UNUSED(widget);
}

void Human::setGender(bool gender)
{
    this->m_gender = gender;
}

void Human::contrPlayer()
{
    ContrlObject::contrlPlayer();

    QRectF zona(this->x()-40,this->y()-40,80,80);
    if(!this->m_gender)
        if(!scene()->items(zona).isEmpty() && scene()->items(zona).size() > 1)
        {
            birth();
         }

    if(this->move)
    {
          m_countForSteps++;
          m_steps = (m_countForSteps/4)%COUNT_IMG;
          m_countForSteps = m_countForSteps % (COUNT_IMG*4);
          update(boundingRect());
    }
    else
    {
        m_steps = 0;
        m_countForSteps =0;
        update(boundingRect());
    }

}

void Human::contrlNPS()
{
        ContrlObject::contrlNPS();

        QRectF zona(this->x()-40,this->y()-40,80,80);

        if(!this->m_gender)
            if(!scene()->items(zona).isEmpty() && scene()->items(zona).size() > 1)
            {
                birth();
            }

        this->m_direction = m_directionOfTravel;

        m_countForSteps++;
        m_steps = (m_countForSteps/4)%COUNT_IMG;
        m_countForSteps = m_countForSteps % (COUNT_IMG*4);
        update(boundingRect());

        m_stepNPS--;
}

void Human::birth()
{
    srand(clock()*(long)(this->x() - this->y()));

    QRectF zona(this->x()-35,this->y()-35,70,70);

    foreach (auto x,  scene()->items(zona))
    {
        if (x == this)
            continue;

      Human* per = dynamic_cast<Human*>(x);

      if(per == nullptr)
          continue;

      if((per->m_gender xor this->m_gender) && this->m_ability && per->m_ability)
      {
          Human *newPer = new Human(rand()%2);
          newPer->setPos(this->x()+(-2*this->m_widthPNG + rand()%(int)4*this->m_widthPNG) ,this->y()+(-2*this->m_heightPNG + rand()%(int)4*this->m_heightPNG));
          scene()->addItem(newPer);

            foreach(auto var,this->listDisease)
            {
                if((rand()/(float)RAND_MAX)<var.getProbabilitySick())
                {
                    newPer->addDisease(var);
                }
            }

            foreach(auto var,per->listDisease)
            {
                if((rand()/(float)RAND_MAX)<var.getProbabilitySick())
                {
                    newPer->addDisease(var);
                }
            }

          this->listNewPer.append(newPer);

          this->m_ability = false;
          per->m_ability = false;

          this->m_birthTimer = new QTimer();
          per->m_birthTimer = new QTimer();

          this->m_timerBorntConnect = connect(this->m_birthTimer,&QTimer::timeout,this,&Human::disbirth);
          per->m_timerBorntConnect = connect(per->m_birthTimer,&QTimer::timeout,per,&Human::disbirth);

          this->m_birthTimer->start(30000);
          per->m_birthTimer->start(30000);

          continue;
      }
   }
}

void Human::disbirth()
{
    delete this->m_birthTimer;
    this->m_birthTimer = nullptr;
    this->m_ability = true;
    disconnect(m_timerBorntConnect);
}

QList<Human*> Human::getListHuman()
{
    return Human::listNewPer;
}

void Human::clearListHuman()
{
    Human::listNewPer.clear();
}

void Human::isHealthy()
{
    srand(clock()*(long)(this->x() - this->y()));

    if(this->listDisease.isEmpty())
        return;

    foreach (auto ill, listDisease)
    {
        this->damage(ill.getDamage());

        QRectF zona(this->x()-ill.getRadiusOfAction(),this->y()-ill.getRadiusOfAction(),ill.getRadiusOfAction()*2,ill.getRadiusOfAction()*2);

        foreach (auto x,  scene()->items(zona))
        {
            if (x == this)
                continue;

           if((rand()/(float)RAND_MAX)< ill.getProbabilitySick())
           {
               Human* per = dynamic_cast<Human*>(x);

               if(per == nullptr)
                   continue;

                per->addDisease(ill);
           }
        }
    }
}

Human::~Human()
{
    for (int j = 0 ; j < COUNT_ANIMATION; j++)
        for (int i = 0; i < COUNT_IMG; i++)
           delete m_animation[j][i];

    disconnect(m_myConnect);
    disconnect(m_timerBorntConnect);
    disconnect(this->m_ConnectHeal);

    if (m_InjectorTimer != nullptr)
             delete m_InjectorTimer;

    if (m_gameTimer != nullptr)
             delete m_gameTimer;

    if (m_birthTimer != nullptr)
             delete m_birthTimer;

    listNewPer.clear();
}

void Human::healHum(int uHeal)
{
    this->hp += uHeal;
    if (hp > 100)
    {
        hp = 100;
    }
}

void Human::damage(int uDamage)
{
    this->hp -= uDamage;
    if (hp <= 0)
    {
        hp = 0;
        this->m_alive = false;
    }
}

bool Human::getStatus()
{
    return this->m_alive;
}

QList<Human *> Human::listNewPer;
