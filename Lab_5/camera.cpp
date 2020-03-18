#include "camera.h"

Camera::Camera(QObject *parent): ContrlObject(parent)
{
    this->step = 5;
    setRotation(0);
    gameTimer = nullptr;
}

QRectF Camera::boundingRect() const
{
    return QRectF(0,0,0,0);
}

void Camera::paint(QPainter *painter, const QStyleOptionGraphicsItem *option, QWidget *widget)
{
        Q_UNUSED(option);
        Q_UNUSED(widget);
}

void Camera::setMovement(bool b_Movement)
{
 if (b_Movement)
 {
     gameTimer = new QTimer();
     myConnect = connect(gameTimer, &QTimer::timeout, this, &Camera::contrPlayer);
     gameTimer->start(10);

 }
 else
 {
    disconnect(myConnect);
    if (gameTimer != nullptr)
         delete gameTimer;
 }
}

void Camera::contrPlayer()
{
        ContrlObject::contrlPlayer();

                 if(this->x() - 10 < -1000)
                 {
                       this->setX(-990);
                 }

                 if(this->x() + 10 > 1000)
                 {
                     this->setX(990);
                 }

                 if(this->y() - 10 < -1000)
                 {
                    this->setY(-990);
                 }

                 if(this->y() + 10 > 1000)
                 {
                     this->setY(990);
                 }

}
Camera::~Camera()
{
    disconnect(myConnect);
    delete this->gameTimer;
}

