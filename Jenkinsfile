pipeline {
    agent any

    environment {
        COMPOSE_PROJECT_NAME = "nayaview"
    }

    stages {
        stage('Checkout') {
            steps {
                git branch: 'main', url: 'https://github.com/lionlightheart/NayaViewAnime.git'
            }
        }

        stage('Build and Deploy Docker Compose') {
            steps {
                withCredentials([string(credentialsId: 'postgres_password', variable: 'POSTGRES_PASSWORD')]) {
                    sh """
                    export POSTGRES_USER=Naya_DB_USER
                    export POSTGRES_PASSWORD=$POSTGRES_PASSWORD
                    export POSTGRES_DB=NayaDb
                    export POSTGRES_PORT=5432
                    export REDIS_HOST=redis
                    export REDIS_PORT=6379
                    export DATABASE_URL=postgres://Naya_DB_USER:$POSTGRES_PASSWORD@db:5432/NayaDb
                    export REDIS_URL=redis://redis:6379

                    docker-compose up -d --build
                    """
                }
            }
        }
    }
}
