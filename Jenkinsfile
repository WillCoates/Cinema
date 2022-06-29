pipeline {
    agent any
    
    stages {
        stage('Build') {
            steps {
                sh 'dotnet publish --configuration:Release'
            }
        }
        
        stage('Test') {
            steps {
                sh 'dotnet test --logger:junit'
                junit '**/TestResults/TestResults.xml'
            }
        }
    }
}
