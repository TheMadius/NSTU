#include "gamewindow.h"
#include "ui_gamewindow.h"

GameWindow::GameWindow(QWidget *parent) : QMainWindow(parent), ui(new Ui::GameWindow)
{
    this->arrDisease = new Disease*[5];
        arrDisease[0]= new Disease("ODS",0.8,80,0,0.6,0.8);
        arrDisease[1]= new Disease("flu",0.5,70,7,0.4,0.6);
        arrDisease[2]= new Disease("Cancer",0.5,0,20,0.8,0);
        arrDisease[4]= new Disease("Paralysis",0,0,5,0,0.3);
        arrDisease[3]= new Disease("Hyperactivity",1.5,50,0,0.01,0.01);

    srand(time(NULL));

    this->init();
    this->cam = new Camera();
    this->player = this->cam;

    scene->addItem(player);

    scene->addRect(-1400,1300,2*1400,10,QPen(Qt::NoPen),QBrush(Qt::NonModal));

    scene->addRect(1400,-1300,10,2*1300,QPen(Qt::NoPen),QBrush(Qt::NonModal));

    scene->addRect(-1400,-1310,2*1400,10,QPen(Qt::NoPen),QBrush(Qt::NonModal));

    scene->addRect(-1410,-1300,10,2*1300,QPen(Qt::NoPen),QBrush(Qt::NonModal));

    player->setPos(0,0);
    player->setMovement(true);

    timer = new QTimer();
    connect(timer,&QTimer::timeout,this,&GameWindow::gameTimerPositeon);
    connect(scene,&CustomScene::signalTargetCoordinate,this,&GameWindow::slotTarget);
    timer->start(1);

    for(int i = 0;i < START_COUNT_DOCTOR;i++)
            addDoctor(-1400 + rand()%2800,-1300 + rand()%2600);

    for(int i = 0;i < START_COUNT_HUMAN;i++)
            addNPS(-1400 + rand()%2800,-1300 + rand()%2600);

    foreach (auto var, this->NPS)
    {
        if ((rand()/(float)RAND_MAX) < 0.5)
            var->addDisease(*arrDisease[rand()%5]);
    }
}

void GameWindow::init()
{
    ui->setupUi(this);
    this->resize(800,600);
    this->setFixedSize(800,600);

    this->scene = new CustomScene();
    this->setWindowIcon(QIcon(":/resourse/iconMain.png"));
    this->setWindowTitle("Torment");
    this->ui->graphicsView->setScene(scene);
    this->ui->graphicsView->setRenderHint(QPainter::Antialiasing);
    this->ui->graphicsView->setVerticalScrollBarPolicy(Qt::ScrollBarAlwaysOff);
    this->ui->graphicsView->setHorizontalScrollBarPolicy(Qt::ScrollBarAlwaysOff);
    this->ui->graphicsView->setMouseTracking(true);

    scene->setSceneRect(-1400,-1300,2800,2600);

    QBrush *ibrush = new QBrush;

    ibrush->setTextureImage(QImage(":/resourse/backgr.jpg"));

    scene->setBackgroundBrush(*ibrush);

}

void GameWindow::slotTarget(QPointF point)
{

   /*if(!this->scene->items(point).isEmpty())
    {
        player->setMovement(false);
        player = (Player *)this->scene->items(point)[0];
        player->setMovement(true);
    }*/


    foreach (Human *targ, this->NPS)
    {
        if (targ == player)
            continue;
        QRectF rect = targ->boundingRect();
        qreal _x = point.x() - targ->x();
        qreal _y = point.y() - targ->y();

        if (_x >= rect.left() && _x <= (rect.width() + rect.left()) && _y >= rect.top() && _y <= (rect.height() + rect.top()))
        {
            player->setMovement(false);
            player = targ;
            player->setMovement(true);
        }

    }
}

void GameWindow::addNPS(qreal x,qreal y)
 {
     srand(clock()*(long)(abs(x - y)));

     Human *per = new Human(rand()%2);

     scene->addItem(per);

     per->setPos(x,y);

     NPS.append(per);
 }

void GameWindow::addDoctor(qreal x,qreal y)
 {

     Doctor *per = new Doctor();

     scene->addItem(per);

     per->setPos(x,y);

     NPS.append(per);
 }

void GameWindow::gameTimerPositeon()
{    
    Human *temp;

    if(GetAsyncKeyState(Qt::Key_Q))
    {
            player->setMovement(false);
            cam->setPos(player->x(),player->y());
            player = cam;
            player->setMovement(true);
    }

    foreach(auto x,Human::getListHuman())
    {
        this->NPS.append(x);
    }

    if(GetAsyncKeyState(Qt::Key_R))
    {
        this->Restart();
    }

    for(int i = NPS.size()-1; i >= 0 ;--i)
        if(!NPS[i]->getStatus())
        {
             temp = NPS[i];
             if(temp == player)
                {
                     player->setMovement(false);
                     cam->setPos(player->x(),player->y());
                     player = cam;
                     player->setMovement(true);
                }
             NPS.removeAt(i);
             scene->removeItem(temp);
             delete temp;
        }

    Human::clearListHuman();

    this->scene->update(QRectF(-1400,-1300,2800,2600));

    this->ui->graphicsView->centerOn(this->player);
}

void GameWindow::Restart()
{
    Human *temp;
    for (int i = NPS.size()-1; i >= 0;--i)
    {
        temp = NPS[i];
        if(temp == player)
           {
                player->setMovement(false);
                cam->setPos(player->x(),player->y());
                player = cam;
                player->setMovement(true);
           }
        NPS.removeAt(i);
        scene->removeItem(temp);
        delete temp;
    }

    NPS.clear();

    for(int i = 0;i < START_COUNT_DOCTOR;i++)
            addDoctor(-1400 + rand()%2800,-1300 + rand()%2600);

    for(int i = 0;i < START_COUNT_HUMAN;i++)
            addNPS(-1400 + rand()%2800,-1300 + rand()%2600);

    foreach (auto var, this->NPS)
    {
        if ((rand()/(float)RAND_MAX) < 0.5)
            var->addDisease(*arrDisease[rand()%5]);
    }
}

GameWindow::~GameWindow()
{
    delete ui;
    delete player;
    delete timer;
    delete scene;
    delete cam;
    delete timer;
    NPS.clear();

    disconnect(myConnect);
}

QList<Human *> GameWindow::NPS;
