import { Configuration, OpenAIApi } from "openai";
import { OpenAIEmbeddings } from "langchain/embeddings/openai";
import { FaissStore } from "langchain/vectorstores/faiss";

const query = "get me a database file with FirstName Jan"
const embedding = await createEmbedding(query)

async function findSimilarDocuments(embedding) {
  try{
    const documents = await collection
          .aggregate([
            {
              $search: {
                knnBeta: {
                  vector: embedding,
                  // path is the path to the embedding field in the mongodb collection documentupload
                  path: 'embedding',
                  // change k to the number of documents you want to be returned
                  k: 2,
                },
              },
            },
            {
              $project: {
                description: 1,
                score: { $meta: 'searchScore' },
              },
            },
          ])
          .toArray()

        return documents
      } catch (err) {
        console.error(err)
      }
    }


//ééééééééééééééé
/*
  try {
    const { query } = req.body

    const embedding = await createEmbedding(query)

    async function findSimilarDocuments(embedding) {
      try {
        // Query for similar documents.
        const documents = await collection
          .aggregate([
            {
              $search: {
                knnBeta: {
                  vector: embedding,
                  // path is the path to the embedding field in the mongodb collection documentupload
                  path: 'embedding',
                  // change k to the number of documents you want to be returned
                  k: 5,
                },
              },
            },
            {
              $project: {
                description: 1,
                score: { $meta: 'searchScore' },
              },
            },
          ])
          .toArray()

        return documents
      } catch (err) {
        console.error(err)
      }
    }

    const similarDocuments = await findSimilarDocuments(embedding)

    console.log('similarDocuments: ', similarDocuments)

    // gets the document with the highest score
    const highestScoreDoc = similarDocuments.reduce((highest, current) => {
      return highest.score > current.score ? highest : current
    })

    console.log('highestScoreDoc', highestScoreDoc)

    const prompt = `Based on this context: ${highestScoreDoc.description} \n\n Query: ${query} \n\n Answer:`

    const answer = await hitOpenAiApi(prompt)
    console.log('answer: ', answer)
    res.send(answer)
  } catch (err) {
    res.status(500).json({
      error: 'Internal server error',
      message: err.message,
    })
  }
})
*/