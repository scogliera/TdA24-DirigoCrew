//chat history
import { config } from "dotenv";
config();

import { ConversationChain } from "langchain/chains";
import { ChatOpenAI } from "@langchain/openai";
import {
    ChatPromptTemplate,
    HumanMessagePromptTemplate,
    SystemMessagePromptTemplate,
    MessagesPlaceholder,
  } from "langchain/prompts";
  import { BufferMemory } from "langchain/memory";

  const llm = new ChatOpenAI({ temperature: 0,  modelName : "gpt-3.5-turbo"  });

  const chain = new ConversationChain({
    memory: new BufferMemory({ returnMessages: true, memoryKey: "history" }),
    prompt: ChatPrompt,
    llm: chat,
  });
  
  await chain.call({
    input: "What is the capital of Prague?",
  });
  const response2 = await chain.call({
    input: "What is a great place to see there?",
  });

  console.log(response2);