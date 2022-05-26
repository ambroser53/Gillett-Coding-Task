function open_sidebar() {
    document.getElementById('new-nuclide-sidebar').classList.toggle('open');
    document.getElementById("new-nuclide-overlay").style.visibility = "visible";
  }
  
  function close_sidebar(success) {
    document.getElementById("new-nuclide-sidebar").classList.toggle('open');
    document.getElementById("new-nuclide-overlay").style.visibility = "hidden";
    if (!success) {
      document.getElementById("nuclide-name-input").value = "";
      document.getElementById("half-life-input").value = "";
      document.getElementById("activity-input").value = "";
    }
  }
  
  function add_info() {
    let nuclide_info = [];
  
    const nuclide_name = document.getElementById("nuclide-name-input").value;
    nuclide_info[nuclide_info.length] = nuclide_name;
  
    const half_life = document.getElementById("half-life-input").value;
    nuclide_info[nuclide_info.length] = half_life;
  
    const activity = document.getElementById("activity-input").value;
    nuclide_info[nuclide_info.length] = activity;
  
    [1, 10, 100, 1000].forEach(element => {
      var decay_at_day = Math.round(calculate_decay(element, half_life, activity) * 100) / 100;
      nuclide_info[nuclide_info.length] = decay_at_day;
    });
  
    var decay_finished = Math.round(find_day_decay_finished(half_life, activity) * 100) / 100;
    nuclide_info[nuclide_info.length] = decay_finished;
  
    var table = document.getElementById("nuclide-data");
  
    var row = table.insertRow(-1);
    nuclide_info.forEach((element, index) => {
      var cell = row.insertCell(index);
      cell.innerHTML = element;
    });
  
    document.getElementById("nuclide-name-input").value = "";
    document.getElementById("half-life-input").value = "";
    document.getElementById("activity-input").value = "";
  
    var current_decay = parseInt(document.getElementById("info").textContent);
    if (current_decay < decay_finished) {
      document.getElementById("info").textContent = decay_finished;
    }
  }
  
  function calculate_decay(time_days, half_life, activity) {
    return activity * Math.pow(0.5, time_days / half_life);
  }
  
  function find_day_decay_finished(half_life, activity) {
    const time_span_limit = 1000000;
    if (half_life > time_span_limit || activity > time_span_limit) {
      return 1;
    }
    var max_day_decay = 1;
    while (max_day_decay < time_span_limit) {
      if (calculate_decay(max_day_decay, half_life, activity) < 1) {
        break;
      } else {
        max_day_decay *= 10;
      }
    }
  
    var low = 0,
      high = max_day_decay;
    while (low != high) {
      var mid = (low + high) / 2;
      if (calculate_decay(mid, half_life, activity) >= 1) {
        low = mid + 1;
      } else {
        high = mid;
      }
    }
  
    return Math.round(low);
  }
  