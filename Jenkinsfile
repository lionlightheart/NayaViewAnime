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

        stage('Generate .env') {
            steps {
                withCredentials([string(credentialsId: 'postgres_password', variable: 'POSTGRES_PASSWORD')]) {
                    sh '''
                    echo "POSTGRES_USER=postgres" > .env
                    echo "POSTGRES_PASSWORD=$POSTGRES_PASSWORD" >> .env
                    echo "POSTGRES_DB=mydb" >> .env
                    echo "REDIS_HOST=redis" >> .env
                    echo "REDIS_PORT=6379" >> .env
                    '''
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
