<template>
  <div class="app">
    <TopBar></TopBar>
    <!-- 主体内容 -->
    <div class="main-content">
      <SearchBox></SearchBox>
      
    </div>
  </div>
</template>

<script lang="ts">  
import SearchBox from './components/SearchBox.vue'; 
import TopBar from './components/TopBar.vue'; 
import QAcard from './components/QAcard.vue';
 


export default { 
  name: "App", 
  components: { TopBar, SearchBox, QAcard } 
}; 
</script>

<style>
.app {
  display: flex;
  flex-direction: column;
  min-height: 100vh; /* 让整个页面占满屏幕高度 */
  padding: 20px;
  box-sizing: border-box;
}

/* 主体内容区域：纵向居中布局 */
.main-content {
  flex: 1;
  display: flex;
  flex-direction: column;
  justify-content: flex-start;
  align-items: center;
  gap: 80px;
  width: 100%;
  max-width: 800px;
  padding: 10px;
  margin: 140px auto 0;
  box-sizing: border-box;
}

.main-content > * {
  width: 100%;
  max-width: 800px;
}
</style>
