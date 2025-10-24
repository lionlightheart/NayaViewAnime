pipeline {
    agent any

    environment {
        COMPOSE_PROJECT_NAME = "nayaview"
        ENV_FILE = ".env"
        CERT_DIR = "certs"
    }

    stages {
        stage('Checkout') {
            steps {
                git branch: 'main', url: 'https://github.com/lionlightheart/NayaViewAnime.git'
            }
        }

        stages {
        stage('Generate .env') {
            steps {
                withCredentials([string(credentialsId: 'postgres_password', variable: 'POSTGRES_PASSWORD')]) {
                    sh '''
                    echo "# PostgreSQL" > $ENV_FILE
                    echo "POSTGRES_USER=Naya_DB_USER" >> $ENV_FILE
                    echo "POSTGRES_PASSWORD=$POSTGRES_PASSWORD" >> $ENV_FILE
                    echo "POSTGRES_DB=NayaDb" >> $ENV_FILE
                    echo "POSTGRES_PORT=5432" >> $ENV_FILE

                    echo "" >> $ENV_FILE
                    echo "# Redis" >> $ENV_FILE
                    echo "REDIS_HOST=redis" >> $ENV_FILE
                    echo "REDIS_PORT=6379" >> $ENV_FILE

                    echo "" >> $ENV_FILE
                    echo "# Backend" >> $ENV_FILE
                    echo "DATABASE_URL=postgres://Naya_DB_USER:$POSTGRES_PASSWORD@db:5432/NayaDb" >> $ENV_FILE
                    echo "REDIS_URL=redis://redis:6379" >> $ENV_FILE
                    '''
                }
            }
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

        stage('Pull Latest Changes') {
            steps {
                sh 'git pull origin main'
            }
        }

        stage('Build Docker Images') {
            steps {
                sh 'docker-compose --env-file $ENV_FILE build'
            }
        }

        stage('Run Tests') {
            steps {
                sh 'docker-compose run --rm backend npm run test'
                sh 'docker-compose run --rm frontend npm run test'
                sh 'docker-compose run --rm analytics pytest'
            }
        }

        stage('Deploy') {
            steps {
                sh 'docker-compose --env-file $ENV_FILE up -d'
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
