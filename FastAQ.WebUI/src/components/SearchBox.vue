<template>
  <div class="search-page">
    <!-- 搜索框 -->
    <div class="search-box">
      <i class="fa-solid fa-magnifying-glass search-icon" aria-hidden="true"></i>
      <input
        type="text"
        class="search-input"
        v-model="query"
        placeholder="快速找到答案..."
        aria-label="Search questions and answers"
      />
    </div>

    <!-- 搜索结果 -->
    <div class="results">
      <div v-if="loading" class="loading">正在搜索...</div>
      <div v-else-if="results.length === 0 && query">
        <p class="no-result">未找到相关结果</p>
      </div>
      <div v-else>
        <div
          v-for="(item, index) in results"
          :key="index"
          class="result-card"
        >
          <h3 class="question">{{ item.question }}</h3>
          <p class="answer">{{ item.answer }}</p>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, watch } from "vue";

defineOptions({ name: "SearchPage" });

const query = ref("");
const results = ref<{ question: string; answer: string }[]>([]);
const loading = ref(false);

// 防抖定时器
let timer: number | null = null;

watch(query, (newVal) => {
  if (timer) clearTimeout(timer);
  if (!newVal.trim()) {
    results.value = [];
    return;
  }
  timer = setTimeout(() => {
    search(newVal);
  }, 50) as unknown as number; // 防抖 0.5 秒
});

const search = async (keyword: string) => {
  loading.value = true;
  try {
    const resp = await fetch(
      `/api/get_qestions?keyword=${encodeURIComponent(keyword)}`
    );
    const data = await resp.json();
    if (data.is_action_success && data.result?.hits?.hits) {
      results.value = data.result.hits.hits.map(
        (h: any) => h._source
      );
    } else {
      results.value = [];
    }

  } catch (err) {
    console.error("搜索失败:", err);
    results.value = [];
  } finally {
    loading.value = false;
  }
};

</script>

<style scoped>
@import url("https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.0/css/all.min.css");

/* 页面布局 */
.search-page {
  display: flex;
  flex-direction: column;
  align-items: center;
  padding: 10% 16px;
}

/* 搜索框 */
.search-box {
  display: flex;
  align-items: center;
  gap: 10px;
  width: 100%;
  max-width: 600px;
  padding: 12px 16px;
  border-radius: 14px;
  background: rgba(255, 255, 255, 0.9);
  -webkit-backdrop-filter: blur(12px);
  backdrop-filter: blur(12px);
  border: 1px solid #e2e8f0;
  box-shadow: 0 2px 8px rgba(15, 23, 42, 0.08);
  transition: all 0.25s ease;
  margin-bottom: 24px;
}

.search-box:focus-within {
  border-color: #0284c7;
  box-shadow: 0 4px 16px rgba(2, 132, 199, 0.15);
  background: rgba(255, 255, 255, 0.95);
}

.search-icon {
  font-size: 18px;
  color: #64748b;
}

.search-input {
  flex: 1;
  border: none;
  outline: none;
  font-size: 16px;
  font-family: 'Inter', 'Segoe UI', Roboto, sans-serif;
  color: #0f172a;
  background: transparent;
}

.search-input::placeholder {
  color: #94a3b8;
}

/* 搜索结果 */
.results {
  width: 100%;
  max-width: 720px;
}

.result-card {
  background: white;
  border: 1px solid #e2e8f0;
  border-radius: 12px;
  padding: 16px 20px;
  margin-bottom: 16px;
  box-shadow: 0 2px 6px rgba(15, 23, 42, 0.05);
  transition: transform 0.2s ease;
}

.result-card:hover {
  transform: translateY(-2px);
}

.question {
  font-size: 18px;
  font-weight: 600;
  margin-bottom: 8px;
  color: #0f172a;
}

.answer {
  font-size: 15px;
  color: #334155;
  line-height: 1.6;
}

.loading {
  text-align: center;
  color: #0284c7;
  font-size: 15px;
}

.no-result {
  text-align: center;
  color: #94a3b8;
  font-size: 15px;
}
</style>
