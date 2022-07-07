pipeline {
    agent {
        docker {
            image 'mcr.microsoft.com/dotnet/sdk:6.0'
            registryUrl 'https://mcr.microsoft.com'
        }
    }
    
    stages {
        stage('Build') {
            steps {
                sh 'dotnet publish --configuration:Release'
            }
        }
        
        stage('Test') {
            steps {
                sh 'dotnet test --logger:junit || true'
                junit '**/TestResults/TestResults.xml'
            }
        }
    }
}
