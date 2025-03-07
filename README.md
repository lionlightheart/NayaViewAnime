# Project Name

A brief description of the project.

## Requirements

Before running the project, make sure you have the following requirements:

- **.NET SDK 8.0 or higher**: [Download from the official site](https://dotnet.microsoft.com/download/dotnet)
- **Database**: If the project uses a database, ensure it is set up. You may use:
  - MongoDb Server Community Edition: [Oficial site](https://www.mongodb.com/products/self-managed/community-edition)
  - You can also use other MongoDB products, such as MongoDB Atlas or MongoDB Enterprise Advanced, but I recommend the Community version.
- **Additional tools** (if any, for example, Redis, RabbitMQ, etc.)

## Clone the Repository

To clone the repository to your local machine, run the following command:

```bash
git clone https://github.com/username/repository.git
cd repository
````

*1. Install Dependencies
Run the following command to restore the project dependencies:

```bash
dotnet restore
```

* 2. Configure Environment Variables
The project may require some environment variables for configuration (e.g., database connection strings or API keys). Create an .env file or set these variables in your local environment.

> [!WARNING] 
>## ⚠️ Important: Using the MyAnimeList API  
>This project uses the MyAnimeList API (**MAL API**) to retrieve anime and manga information. **It does not include pre-downloaded data or credentials.**  
>
>### 🚀 How to Use This Project  
>1. **Obtain a Client ID**:  
>   - To access the MAL API, you need to register and get a `Client ID` from [their official page](https://myanimelist.net/).  
>   - This `Client ID` must be set in a `.env` file.  
>
>2. **Set Up Your Database**:  
>   - This project **does not include a database with extracted MAL data**.  
>   - You must configure your own database and populate it with your own API requests.  
>
>### ❌ What This Project Does NOT Do  
>- It does not redistribute extracted MAL data.  
>- It does not provide credentials or API keys.  
>- It does not allow API requests to MAL without setting up your own `Client ID`.  
>
>### 📜 Legal Notice  
>The use of the MAL API is subject to their **[Terms and Conditions](https://myanimelist.net/static/apiagreement.html)**. Make sure to comply with them when using this project.  

## License

This project is licensed under the GNU General Public License v3.0 (GPL-3.0). You can view the full text of the license in the [LICENSE](LICENSE) file.

### Terms:

- Permission is granted to use, copy, modify, and distribute the code of this project.
- Any derivative work must be published under the same GPL-3.0 license.
- The code must retain attribution to the original author.

For more details, see the [LICENSE](LICENSE) file.
