pipeline {
    agent any

    environment {
        COMPOSE_PROJECT_NAME = "nayaview"
        CERT_DIR = "certs"
    }

    stages {
        stage('Checkout') {
            steps {
                git branch: 'main', url: 'https://github.com/lionlightheart/NayaViewAnime.git'
            }
        }

        stage('Generate SSL Certificates') {
            steps {
                sh """
                mkdir -p $CERT_DIR
                openssl req -x509 -nodes -days 365 \
                    -subj '/CN=localhost/O=Dev' \
                    -newkey rsa:2048 \
                    -keyout $CERT_DIR/cert.key \
                    -out $CERT_DIR/cert.crt
                """
            }
        }

        stage('Build and Deploy Docker Compose') {
            steps {
                withCredentials([string(credentialsId: 'postgres_password', variable: 'POSTGRES_PASSWORD')]) {
                    sh """
                    # Export variables en memoria
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

        stage('Run Tests') {
            steps {
                sh 'docker-compose run --rm backend npm run test'
                sh 'docker-compose run --rm frontend npm run test'
                sh 'docker-compose run --rm analytics pytest'
            }
        }
    }

    post {
        success {
            echo '✅ Despliegue exitoso'
        }
        failure {
            echo '❌ Fallo en el pipeline'
        }
    }
}
