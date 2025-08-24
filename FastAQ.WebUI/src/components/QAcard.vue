<template>
  <div class="qa-card">
    <!-- 问题 -->
    <div class="question">
      <span class="q-label">Q</span>
      <p class="q-text">{{ question }}</p>
    </div>

    <!-- 答案 -->
    <div class="answer">
      <span class="a-label">A</span>
      <p class="a-text">{{ answer }}</p>
    </div>
  </div>
</template>

<script lang="ts">
export default {
  name: "QACard",
  props: {
    question: { type: String, required: true },
    answer: { type: String, required: true }
  }
};
</script>

<style scoped>
.qa-card {
  width: 100%;
  
  
  padding: 24px 24px;

  background: rgba(255, 255, 255, 0.7);
  backdrop-filter: blur(12px) saturate(150%);
  -webkit-backdrop-filter: blur(12px) saturate(150%);
  border-radius: 16px;

  box-shadow: 0 4px 20px rgba(0, 0, 0, 0.08);

  display: flex;
  flex-direction: column;
  gap: 16px;

  transition: transform 0.25s ease, box-shadow 0.25s ease;
}
.qa-card:hover {
  transform: translateY(-4px);
  box-shadow: 0 6px 24px rgba(0, 0, 0, 0.12);
}

/* Q */
.question {
  display: flex;
  align-items: flex-start;
  gap: 10px;
}
.q-label {
  font-weight: 700;
  font-size: 20px;
  color: #ff6b6b;
}
.q-text {
  font-size: 16px;
  font-weight: 600;
  color: #222;
  margin: 0;
  line-height: 1.6;
}

/* A */
.answer {
  display: flex;
  align-items: flex-start;
  gap: 10px;
}
.a-label {
  font-weight: 700;
  font-size: 20px;
  color: #1e90ff;
}
.a-text {
  font-size: 15px;
  font-weight: 400;
  color: #444;
  margin: 0;
  line-height: 1.6;
}
</style>
