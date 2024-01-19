//reading from documents
import { config } from "dotenv";
config();

import { OpenAIEmbeddings } from "@langchain/openai";
import { FaissStore } from "@langchain/community/vectorstores/faiss";
import { OpenAI } from "@langchain/openai";
import { RetrievalQAChain, loadQAStuffChain } from "langchain/chains";

const embeddings = new OpenAIEmbeddings();
const vectorStore = await FaissStore.load("./", embeddings);

const model = new OpenAI({ temperature: 0,  modelName : "gpt-3.5-turbo"  });

const chain = new RetrievalQAChain({
    combineDocumentsChain: loadQAStuffChain(model),
    retriever: vectorStore.asRetriever(1),
    returnSourceDocuments: true,
});

const res = await chain.invoke({
    query: "Which lecturer would you choose for someone with a low budget?",
});
console.log(res.text);