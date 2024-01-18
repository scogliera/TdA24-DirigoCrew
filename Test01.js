//basic chain

import { config } from "dotenv";
config();

import { OpenAI } from "langchain/llms/openai";
import { PromptTemplate } from "langchain/prompts";
import { LLMChain } from "langchain/chains";

const model = new OpenAI({ temperature: 0, modelName : "gpt-3.5-turbo" });

const template = "Be funny when answering \n Question: {question}";
const prompt = new PromptTemplate({ template, inputVariables: ["question"] });

const chain = new LLMChain({ llm: model, prompt });

const result = await chain.call({ question: "Jason, come, quickly! We don't have much time! They're in the fucking walls!"});
console.log(result);